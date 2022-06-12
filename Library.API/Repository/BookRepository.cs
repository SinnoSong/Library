using Library.API.Entities;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Book> GetBookAsync(string author, Guid bookId)
        {
            return await Table.SingleOrDefaultAsync(Book => Book.Author == author && Book.Id == bookId);
        }

        public Task<IEnumerable<Book>> GetBooksAsync(string author)
        {
            return Task.FromResult(Table.Where(book => book.Author == author).AsEnumerable());
        }
    }
}