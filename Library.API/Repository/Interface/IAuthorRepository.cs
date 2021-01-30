using Library.API.Entities;
using System;

namespace Library.API.Repository.Interface
{
    public interface IAuthorRepository : IRepositoryBase<Author, Guid>
    {
    }
}