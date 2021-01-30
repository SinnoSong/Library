using Library.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.API.Configs.Filters;
using Library.API.Repository.Interface;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.Entities;

namespace Library.API.Controllers
{
    [Route("api/authors/{authorId}/books")]
    [ApiController]
    [ServiceFilter(typeof(CheckAuthorExistFilterAttribute))]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public BookController(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            this._bookRepository = repositoryWrapper.Book;
            this._authorRepository = repositoryWrapper.Author;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookDto>>> GetBooksAsync(Guid authorId)
        {
            var books = await _bookRepository.GetBooksAsync(authorId);
            var bookDtoList = _mapper.Map<IEnumerable<BookDto>>(books);
            return bookDtoList.ToList();
        }

        [HttpGet("{bookId}", Name = nameof(GetBookAsync))]
        public async Task<ActionResult<BookDto>> GetBookAsync(Guid authorId, Guid bookId)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
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
        public async Task<IActionResult> UpdateBookAsync(Guid authorId, Guid bookId, BookForUpdateDto updateBook)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
            }
            _mapper.Map(updateBook, book, typeof(BookForUpdateDto), typeof(Book));
            _bookRepository.Update(book);
            var result = await _bookRepository.SaveAsync();
            if (!result)
            {
                throw new Exception("更新资源Book失败");
            }
            return NoContent();
        }

        [HttpPatch("{bookId}")]
        public async Task<IActionResult> PartiallyUpdateBookAsync(Guid authorId, Guid bookId, JsonPatchDocument<BookForUpdateDto> patchDocument)
        {
            var book = await _bookRepository.GetBookAsync(authorId, bookId);
            if (book == null)
            {
                return NotFound();
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
            return NoContent();
        }
    }
}