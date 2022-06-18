using Library.API.Entities;
using Library.API.Repository.Interface;
using Library.API.Service.Interface;
using System;

namespace Library.API.Service
{
    public class BookService : BaseService<Book, Guid>, IBookService
    {
        public BookService(IBaseRepository<Book, Guid> dal)
        {
            BaseDal = dal;
        }
    }
}
