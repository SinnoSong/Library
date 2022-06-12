using Library.API.Entities;
using Library.API.Models;
using Library.API.Repository.Interface;
using Library.API.Services.Interface;
using System;
using System.Collections.Generic;

namespace Library.API.Services
{
    public class BookServices : BaseServices<Book, Guid>, IBookServices
    {
        public BookServices(IBaseRepository<Book, Guid> dal)
        {
            BaseDal = dal;
        }

        public void AddBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public void DeleteBook(BookDto book)
        {
            throw new NotImplementedException();
        }

        public BookDto GetBookForAuthor(Guid authorId, Guid bookId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BookDto> GetBooksForAuthor(Guid authorId)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Guid authorId, Guid bookId, BookForUpdateDto book)
        {
            throw new NotImplementedException();
        }
    }
}
