using AutoMapper;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Helper;
using Library.API.Models;
using Library.API.Services.Interface;
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
    [Route("api/books")]
    [ApiController]
    [AllowAnonymous]
    //[Authorize]
    //[ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly HashFactory _hashFactory;

        public BookController(IServicesWrapper repositoryWrapper, IMapper mapper,
            IMemoryCache memoryCache, HashFactory hashFactory)
        {
            _bookServices = repositoryWrapper.Book;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _hashFactory = hashFactory;
        }

        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<PagedList<BookDto>>> GetBooksAsync(string? title = null, string? author = null, string? ibsn = null, Guid? category = null, int page = 0, int pageSize = 25)
        {
            var books = await _bookServices.GetAllAsync();
            if (books != null)
            {
                if (category != null)
                {
                    books = books.Where(book => book.CategoryId == category);
                }
                if (title != null)
                {
                    books = books.Where(book => book.Title == title || book.Title.Contains(title));
                }
                if (author != null)
                {
                    books = books.Where(book => book.Author == author || book.Author.Contains(author));
                }
                if (ibsn != null)
                {
                    books = books.Where(book => book.Isbn == ibsn);
                }
            }
            else
            {
                throw new ArgumentException("当前没有书籍在库");
            }
            // todo mapper异常
            var bookDtoList = _mapper.Map<IQueryable<Book>, IQueryable<BookDto>>(books);
            return await PagedList<BookDto>.CreateAsync(bookDtoList, page, pageSize);
        }

        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid bookId)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityNewHash = _hashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            var bookDto = _mapper.Map<BookDto>(book);
            return bookDto;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookAsync(Guid authorId, BookForCreationDto bookForCreationDto)
        {
            var book = _mapper.Map<Book>(bookForCreationDto);
            var result = await _bookServices.AddAsync(book);
            if (result == null)
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDto = _mapper.Map<BookDto>(book);
            return CreatedAtRoute(nameof(GetBookAsync), new { bookId = bookDto.Id }, bookDto);
        }

        [HttpPost("books")]
        public async Task<IActionResult> AddBooksAsync(string author, BookForCreationCollectionDto Books)
        {
            var books = _mapper.Map<IEnumerable<Book>>(Books.Books);
            foreach (var book in books)
            {
                book.Author = author;
                await _bookServices.AddAsync(book);
            }
            var result = await _bookServices.SaveAsync();
            if (!result)
            {
                throw new Exception("创建资源Book失败");
            }
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books).ToList();
            return CreatedAtRoute(nameof(GetBooksAsync), new { author }, bookDtoList);
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookAsync(Guid bookId)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            await _bookServices.DeleteAsync(book);
            var result = await _bookServices.SaveAsync();
            if (!result)
            {
                throw new Exception("删除资源Book失败");
            }
            return NoContent();
        }

        [HttpPut("{bookId}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> UpdateBookAsync(Guid bookId, BookForUpdateDto updateBook)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }
            _mapper.Map(updateBook, book, typeof(BookForUpdateDto), typeof(Book));
            await _bookServices.UpdateAsync(book);
            var entityNewHash = _hashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }

        [HttpPatch("{bookId}")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            if (book == null)
            {
                return NotFound();
            }
            var entityHash = _hashFactory.GetHash(book);
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
            await _bookServices.UpdateAsync(book);
            var entityNewHash = _hashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }
    }
}