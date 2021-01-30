using Library.API.Entities;
using Library.API.Repository.Interface;
using System;

namespace Library.API.Repository
{
    public class BookRepository : RepositoryBase<Book, Guid>, IBookRepository
    {
        public BookRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}