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