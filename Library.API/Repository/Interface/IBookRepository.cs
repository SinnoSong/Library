using Library.API.Entities;
using System;

namespace Library.API.Repository.Interface
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
    }
}