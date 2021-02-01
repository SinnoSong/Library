using Library.API.Configs;
using Library.API.Entities;
using Library.API.Extentions;
using Library.API.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Library.API.Repository
{
    public class AuthorRepository : RepositoryBase<Author, Guid>, IAuthorRepository
    {
        private Dictionary<string, PropertyMapping> mappingDict = null;

        public AuthorRepository(LibraryDbContext dbContext) : base(dbContext)
        {
            mappingDict = new Dictionary<string, PropertyMapping>(StringComparer.OrdinalIgnoreCase)
            {
                { "Name", new PropertyMapping("Name") },
                { "Age", new PropertyMapping("BirthDate", true) },
                { "BirthPlace", new PropertyMapping("BirthPlace") }
            };
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
            var orderAuthors = authors.Sort(parameters.SortBy, mappingDict);
            return PagedList<Author>.CreateAsync(orderAuthors, parameters.PageNumber, parameters.PageSize);
        }
    }
}