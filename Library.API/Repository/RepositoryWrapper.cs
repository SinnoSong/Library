using Library.API.Entities;
using Library.API.Repository.Interface;

namespace Library.API.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IBookRepository _bookRepository = default;

        public RepositoryWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }

        public IBookRepository Book => _bookRepository ?? new BookRepository(LibraryDbContext);

        public LibraryDbContext LibraryDbContext { get; }
    }
}