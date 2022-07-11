using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.API.Configs;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Helper;
using Library.API.Service.Interface;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        #region field

        private readonly IBookService _bookServices;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, PropertyMapping> _mappingDict;

        #endregion

        #region ctor

        public BookController(IServicesWrapper repositoryWrapper, IMapper mapper)
        {
            _bookServices = repositoryWrapper.Book;
            _mapper = mapper;
            _mappingDict = new Dictionary<string, PropertyMapping>
            {
                { "title", new PropertyMapping("Title") },
                { "author", new PropertyMapping("Author") },
                { "ibsn", new PropertyMapping(targetProperty: "Ibsn") },
                { "category", new PropertyMapping(targetProperty: "Category") }
            };
            _categoryService = repositoryWrapper.Category;
        }

        #endregion

        #region Get

        [HttpGet(Name = nameof(GetBooksAsync))]
        public async Task<ActionResult<PagedList<BookDto>>> GetBooksAsync(bool isLend, string sort = "title",
            string? title = null, string? author = null, string? ibsn = null, string? category = null, int page = 1,
            int pageSize = 25)
        {
            Expression<Func<Book, bool>> select = book => book.IsLend == isLend;
            if (category != null)
            {
                var cate = (await _categoryService.GetByConditionAsync(c => c.Name == category)).FirstOrDefault();
                if (cate == null) throw new Exception("category 不存在");
                select = select.And(book => book.CategoryId == cate.Id);
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
            return _mapper.Map<BookDto>(book);
        }

        #endregion

        #region Post

        [HttpPost]
        [Authorize(Roles = "Administrator,SuperAdministrator")]
        public async Task<IActionResult> AddBookAsync(BookCreateDto bookForCreationDto)
        {
            var cate = (await _categoryService.GetByConditionAsync(c => c.Name == bookForCreationDto.Category)).FirstOrDefault();
            if (cate == null)
            {
                throw new Exception("category不能为空");
            }
            var book = _mapper.Map<Book>(bookForCreationDto);
            book.CategoryId = cate.Id;
            var result = await _bookServices.AddAsync(book);
            if (result == null)
            {
                throw new Exception("创建资源Book失败");
            }

            var bookDto = _mapper.Map<BookDto>(result);
            return CreatedAtRoute(nameof(GetBookAsync), new { bookId = bookDto.Id }, bookDto);
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
        public async Task<IActionResult> UpdateBookAsync(Guid bookId, BookUpdateDto updateBook)
        {
            var cate = (await _categoryService.GetByConditionAsync(c => c.Name == updateBook.Category)).FirstOrDefault();
            if (cate == null)
            {
                throw new Exception("category不能为空");
            }
            var book = await _bookServices.GetByIdAsync(bookId);
            book.CategoryId = cate.Id;

            _mapper.Map(updateBook, book);
            await _bookServices.UpdateAsync(book);
            return NoContent();
        }

        #endregion
    }
}