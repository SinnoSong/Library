using Library.API.Entities;
using Library.API.Repository.BASE;
using Library.API.Services.Interface;
using System;

namespace Library.API.Services
{
    public class ServicesWrapper : IServicesWrapper
    {
        private readonly IBookServices _bookServices = default;

        public ServicesWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }
        public IBookServices Book => _bookServices ?? new BookServices(new BaseRepository<Book, Guid>(LibraryDbContext));

        public LibraryDbContext LibraryDbContext { get; }

    }
}