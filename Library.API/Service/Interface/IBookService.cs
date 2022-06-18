using Library.API.Entities;
using System;

namespace Library.API.Service.Interface
{
    public interface IBookService : IBaseService<Book, Guid>
    {
    }
}