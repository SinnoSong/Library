﻿using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.API.Repository.Interface
{
    public interface IBookRepository : IRepositoryBase<Book, Guid>
    {
        Task<IEnumerable<Book>> GetBooksAsync(string author);

        Task<Book> GetBookAsync(string author, Guid bookId);
    }
}