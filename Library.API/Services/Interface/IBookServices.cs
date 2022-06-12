using Library.API.Entities;
using Library.API.Models;
using System;
using System.Collections.Generic;

namespace Library.API.Services.Interface
{
    public interface IBookServices : IBaseServices<Book, Guid>
    {
        IEnumerable<BookDto> GetBooksForAuthor(Guid authorId);

        BookDto GetBookForAuthor(Guid authorId, Guid bookId);

        void AddBook(BookDto book);

        void DeleteBook(BookDto book);

        void UpdateBook(Guid authorId, Guid bookId, BookForUpdateDto book);
    }
}