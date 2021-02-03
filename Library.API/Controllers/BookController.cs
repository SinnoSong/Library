using AutoMapper;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Helper;
using Library.API.Models;
using Library.API.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController, Authorize]
    [ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public BookController(IRepositoryWrapper repositoryWrapper, IMapper mapper,
            IMemoryCache memoryCache)
        {
            this._bookRepository = repositoryWrapper.Book;
            this._authorRepository = repositoryWrapper.Author;
            this._mapper = mapper;
            this._memoryCache = memoryCache;
        }

        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<List<BookDto>>> GetBooksAsync(Guid authorId)
        {
            string key = $"{authorId}_books";
            if (!_memoryCache.TryGetValue(key, out List<BookDto> bookDtoList))
            {
                var books = await _bookRepository.GetBooksAsync(authorId);
                bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books).ToList();
                // 设置缓存有效时间和优先级
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(10),
                    Priority = CacheItemPriority.Normal
                };
                _memoryCache.Set(key, bookDtoList, options);
            }
            return bookDtoList;
        }

        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid authorId, Guid bookId)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityNewHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = _mapper.Map<Book>(bookForCreationDto);
            book.AuthorId = authorId;
            _bookRepository.Create(book);
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDto = _mapper.Map<BookDto>(book);
            return CreatedAtRoute(nameof(GetBookAsync), new { authorId = bookDto.AuthorId, bookId = bookDto.Id }, bookDto);
        }

        [HttpPost("books")]
        public async Task<IActionResult> AddBooksAsync(Guid authorId, BookForCreationCollectionDto Books)
        {
            var books = _mapper.Map<IEnumerable<Book>>(Books.Books);
            foreach (var book in books)
            {
                book.AuthorId = authorId;
                _bookRepository.Create(book);
            }
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books).ToList();
            return CreatedAtRoute(nameof(GetBooksAsync), new { authorId }, bookDtoList);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(Guid authorId, Guid bookId)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            _bookRepository.Delete(book);
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源Book失败");
            }
            return NoContent();
        }

        [HttpPut("{bookId}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updateBook)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityHash = HashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            _mapper.Map(updateBook, book, typeof(BookForUpdateDto), typeof(Book));
            _bookRepository.Update(book);
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("更新资源Book失败");
            }
            var entityNewHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }

        [HttpPatch("{bookId}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityHash = HashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            var bookUpdateDto = _mapper.Map<BookForUpdateDto>(book);
            patchDocument.ApplyTo(bookUpdateDto, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(bookUpdateDto, book, typeof(BookForUpdateDto), typeof(Book));
            _bookRepository.Update(book);
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("更新资源Book失败");
            }
            var entityNewHash = HashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }
    }
}