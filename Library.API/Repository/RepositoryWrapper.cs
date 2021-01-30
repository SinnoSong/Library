using Library.API.Entities;
using Library.API.Repository.Interface;

namespace Library.API.Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly IAuthorRepository _authorRepository = default;
        private readonly IBookRepository _bookRepository = default;

        public RepositoryWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }

        public IBookRepository Book => _bookRepository ?? new BookRepository(LibraryDbContext);

        public IAuthorRepository Author => _authorRepository ?? new AuthorRepository(LibraryDbContext);

        public LibraryDbContext LibraryDbContext { get; }
    }
}