using System;
using Library.API.Entities;

namespace Library.API.Service.Interface
{
    public interface IBookService : IBaseService<Book, Guid>
    {
    }
}