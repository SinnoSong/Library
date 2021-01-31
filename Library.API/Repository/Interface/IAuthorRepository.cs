using Library.API.Configs;
using Library.API.Entities;
using System;
using System.Threading.Tasks;

namespace Library.API.Repository.Interface
{
    public interface IAuthorRepository : IRepositoryBase<Author, Guid>
    {
        Task<PagedList<Author>> GetAllAsync(AuthorResourceParameters parameters);
    }
}