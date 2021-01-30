using Library.API.Data;
using Library.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.API.Services
{
    public class BookMockRepository : IBookRepository
    {
        public void AddBook(BookDto book)
        {
            LibraryMockData.Curent.Books.Add(book);
        }

        public void DeleteBook(BookDto book)
        {
            LibraryMockData.Curent.Books.Remove(book);
        }

        public BookDto GetBookForAuthor(Guid authorId, Guid bookId)
        {
            return LibraryMockData.Curent.Books.FirstOrDefault(b => b.AuthorId == authorId && b.Id == bookId);
        }

        public IEnumerable<BookDto> GetBooksForAuthor(Guid authorId)
        {
            return LibraryMockData.Curent.Books.Where(b => b.AuthorId == authorId).ToList();
        }

        public void UpdateBook(Guid authorId, Guid bookId, BookForUpdateDto book)
        {
            var oldBookd = GetBookForAuthor(authorId, bookId);
            oldBookd.Title = book.Title;
            oldBookd.Description = book.Description;
            oldBookd.Pages = book.Pages;
        }
    }
}