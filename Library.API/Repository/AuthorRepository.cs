using Library.API.Entities;
using Library.API.Repository.Interface;
using System;

namespace Library.API.Repository
{
    public class AuthorRepository : RepositoryBase<Author, Guid>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }
    }
}