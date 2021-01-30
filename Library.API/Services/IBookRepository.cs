using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IBookRepository
    {
        IEnumerable<BookDto> GetBooksForAuthor(Guid authorId);

        BookDto GetBookForAuthor(Guid authorId, Guid bookId);

        void AddBook(BookDto book);

        void DeleteBook(BookDto book);

        void UpdateBook(Guid authorId, Guid bookId, BookForUpdateDto book);
    }
}