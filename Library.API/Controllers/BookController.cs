using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.API.Configs;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Extentions;
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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookServices _bookServices;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly HashFactory _hashFactory;
        private readonly Dictionary<string, PropertyMapping> mappingDict;

        public BookController(IServicesWrapper repositoryWrapper, IMapper mapper,
            IMemoryCache memoryCache, HashFactory hashFactory)
        {
            _bookServices = repositoryWrapper.Book;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _hashFactory = hashFactory;
            mappingDict = new Dictionary<string, PropertyMapping>
            {
                { "title", new PropertyMapping("Title") },
                { "author", new PropertyMapping("Author") },
                { "ibsn", new PropertyMapping(targetProperty: "Ibsn") },
                { "category", new PropertyMapping(targetProperty: "Category") }
            };
        }
        #region Get

        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<PagedList<BookDto>>> GetBooksAsync(string sort, string? title = null, string? author = null, string? ibsn = null, Guid? category = null, int page = 1, int pageSize = 25)
        {
            var books = await _bookServices.GetAllAsync();
            books.Sort(sort, mappingDict);
            // 只能查询
            Expression<Func<Book, bool>> select = book => !book.IsLend;
            if (books != null)
            {
                if (category != null)
                {
                    select = select.And(book => book.CategoryId == category);
                }
                if (title != null)
                {
                    select = select.And(book => book.Title == title || book.Title.Contains(title));
                }
                if (author != null)
                {
                    select = select.And(book => book.Author == author || book.Author.Contains(author));
                }
                if (ibsn != null)
                {
                    select = select.And(book => book.Isbn == ibsn);
                }
            }
            else
            {
                throw new ArgumentException("当前没有书籍在库");
            }
            return await PagedList<BookDto>.CreateAsync(books.ProjectTo<BookDto>(_mapper.ConfigurationProvider), page, pageSize);
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
        #endregion

        #region Post

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddBookAsync(BookForCreationDto bookForCreationDto)
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

        #endregion

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

    }
}