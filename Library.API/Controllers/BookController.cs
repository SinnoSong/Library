using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.API.Configs;
using Library.API.Configs.Filters;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Helper;
using Library.API.Service.Interface;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Controllers
{
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        #region field

        private readonly IBookService _bookServices;
        private readonly IMapper _mapper;
        private readonly HashFactory _hashFactory;
        private readonly Dictionary<string, PropertyMapping> _mappingDict;

        #endregion

        #region ctor

        public BookController(IServicesWrapper repositoryWrapper, IMapper mapper, HashFactory hashFactory)
        {
            _bookServices = repositoryWrapper.Book;
            _mapper = mapper;
            _hashFactory = hashFactory;
            _mappingDict = new Dictionary<string, PropertyMapping>
            {
                { "title", new PropertyMapping("Title") },
                { "author", new PropertyMapping("Author") },
                { "ibsn", new PropertyMapping(targetProperty: "Ibsn") },
                { "category", new PropertyMapping(targetProperty: "Category") }
            };
        }

        #endregion

        #region Get

        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<PagedList<BookDto>>> GetBooksAsync(bool isLend, string sort = "title",
            string? title = null, string? author = null, string? ibsn = null, Guid? category = null, int page = 1,
            int pageSize = 25)
        {
            Expression<Func<Book, bool>> select = book => book.IsLend == isLend;
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

            var books = await _bookServices.GetByConditionAsync(select);
            books = books.Sort(sort, _mappingDict);
            return await PagedList<BookDto>.CreateAsync(books.ProjectTo<BookDto>(_mapper.ConfigurationProvider), page,
                pageSize);
        }

        [HttpGet("{bookId:guid}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid bookId)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            var entityNewHash = _hashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return _mapper.Map<BookDto>(book);
        }

        #endregion

        #region Post

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddBookAsync(BookCreateDto bookForCreationDto)
        {
            var result = await _bookServices.AddAsync(_mapper.Map<Book>(bookForCreationDto));
            if (result == null)
            {
                throw new Exception("创建资源Book失败");
            }

            var bookDto = _mapper.Map<BookDto>(result);
            return CreatedAtRoute(nameof(GetBookAsync), new { bookId = bookDto.Id }, bookDto);
        }

        [HttpPost("books")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddBooksAsync(BookCreateCollectionDto bookCreateCollectionDto)
        {
            var books = _mapper.Map<IEnumerable<Book>>(bookCreateCollectionDto.Books);
            await _bookServices.AddAsync(books);
            if (!await _bookServices.SaveAsync())
            {
                throw new Exception("创建资源Book失败");
            }

            return Ok(bookCreateCollectionDto);
        }

        #endregion

        #region Delete

        [HttpDelete("{bookId:guid}")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> DeleteBookAsync(Guid bookId)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            await _bookServices.DeleteAsync(book);
            return NoContent();
        }

        #endregion

        #region Put

        [HttpPut("{bookId:guid}")]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        [CheckIfMatchHeaderFilter]
        public async Task<IActionResult> UpdateBookAsync(Guid bookId, BookUpdateDto updateBook)
        {
            var book = await _bookServices.GetByIdAsync(bookId);
            var entityHash = _hashFactory.GetHash(book);
            if (Request.Headers.TryGetValue(HeaderNames.IfMatch, out var requestETag) && requestETag != entityHash)
            {
                return StatusCode(StatusCodes.Status412PreconditionFailed);
            }

            _mapper.Map(updateBook, book);
            await _bookServices.UpdateAsync(book);
            var entityNewHash = _hashFactory.GetHash(book);
            Response.Headers[HeaderNames.ETag] = entityNewHash;
            return NoContent();
        }

        #endregion
    }
}