using Library.API.Configs;
using Library.API.Entities;
using Library.API.Repository.Interface;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.API.Repository
{
    public class AuthorRepository : RepositoryBase<Author, Guid>, IAuthorRepository
    {
        public AuthorRepository(LibraryDbContext dbContext) : base(dbContext)
        {
        }

        public Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters)
        {
            IQueryable<Author> authors = Table;
            if (!string.IsNullOrWhiteSpace(parameters.BirthPlace))
            {
                authors = Table.Where(m => m.BirthPlace.ToLower() == parameters.BirthPlace.ToLower());
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                authors = authors.Where(m => m.BirthPlace.ToLower().Contains(parameters.SearchQuery.ToLower()) ||
                                m.Name.ToLower().Contains(parameters.SearchQuery.ToLower()
                ));
            }
            return PagedList<Author>.CreateAsync(authors, parameters.PageNumber, parameters.PageSize);
        }
    }
}