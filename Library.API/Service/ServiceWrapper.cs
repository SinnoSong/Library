using Library.API.Entities;
using Library.API.Repository.BASE;
using Library.API.Service.Interface;
using System;
using Library.API.Repository;

namespace Library.API.Service
{
    public class ServicesWrapper : IServicesWrapper
    {
        private readonly IBookService _bookServices = default;
        private readonly ICategoryService _categoryService = default;
        private readonly ILendConfigService _lendConfigService = default;
        private readonly ILendRecordService _lendRecordService = default;
        private readonly INoticeService _noticeService = default;

        public ServicesWrapper(LibraryDbContext libraryDbContext)
        {
            LibraryDbContext = libraryDbContext;
        }

        public IBookService Book => _bookServices ?? new BookService(new BaseRepository<Book, Guid>(LibraryDbContext));

        public ICategoryService Category =>
            _categoryService ?? new CategoryService(new BaseRepository<Category, Guid>(LibraryDbContext));

        public ILendConfigService LendConfig => _lendConfigService ??
                                                new LendConfigService(
                                                    new BaseRepository<LendConfig, Guid>(LibraryDbContext));

        public ILendRecordService LendRecord =>
            _lendRecordService ?? new LendRecordService(new LendRecordRepository(LibraryDbContext));

        public INoticeService Notice =>
            _noticeService ?? new NoticeService(new BaseRepository<Notice, Guid>(LibraryDbContext));

        public LibraryDbContext LibraryDbContext { get; }
    }
}