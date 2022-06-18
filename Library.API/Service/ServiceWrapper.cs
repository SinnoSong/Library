using Library.API.Entities;
using Library.API.Repository.BASE;
using Library.API.Service.Interface;
using System;

namespace Library.API.Service
{
    public class ServicesWrapper : IServicesWrapper
    {
        private readonly IBookService _bookServices = default;
        private readonly ICategoryService _categoryService = default;
        private readonly ILendConfigService _lendConfigService = default;

        public ServicesWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }
        public IBookService Book => _bookServices ?? new BookService(new BaseRepository<Book, Guid>(LibraryDbContext));
        public ICategoryService Category => _categoryService ?? new CategoryService(new BaseRepository<Category, Guid>(LibraryDbContext));
        public ILendConfigService LendConfig => _lendConfigService ?? new LendConfigService(new BaseRepository<LendConfig, Guid>(LibraryDbContext);

        public LibraryDbContext LibraryDbContext { get; }

    }
}