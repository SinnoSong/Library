using Library.API.Entities;
using Library.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.API.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Book> GetBookAsync(Guid authorId, Guid bookId)
        {
            return await Table.SingleOrDefaultAsync(Book => Book.AuthorId == authorId && Book.Id == bookId);
        }

        public Task<IEnumerable<Book>> GetBooksAsync(Guid authorId)
        {
            return Task.FromResult(Table.Where(book => book.AuthorId == authorId).AsEnumerable());
        }
    }
}