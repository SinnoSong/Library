using Library.API.Entities;
using Library.API.Repository.BASE;
using Library.API.Service.Interface;
using System;

namespace Library.API.Service
{
    public class ServicesWrapper : IServicesWrapper
    {
        private readonly IBookService _bookServices = default;

        public ServicesWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }
        public IBookService Book => _bookServices ?? new BookService(new BaseRepository<Book, Guid>(LibraryDbContext));

        public LibraryDbContext LibraryDbContext { get; }

    }
}