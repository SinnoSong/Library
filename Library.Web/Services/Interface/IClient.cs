using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface IClient
    {
        public HttpClient HttpClient { get; }

        Task<AuthResponse> LoginAsync(LoginUserDto body);
        Task<bool> RegisterUserAsync(RegisterUser registerUser);

        #region book

        Task<BookCreateDto> CreateBookAsync(BookCreateDto body);
        Task DeleteBookAsync(string id);
        Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto);
        Task<List<BookDto>> GetBooksAsync(BookQueryParameters? queryParameters);
        Task<BookDto> GetBookById(string id);

        #endregion

        #region category

        Task<CategoryCreateDto> CreateCategoryAsync(CategoryCreateDto body);
        Task DeleteCategoryAsync(string id);
        Task UpdateCategoryAsync(string id, CategoryCreateDto updateDto);
        Task<List<CategoryDto>> GetCategoriesAsync(CategoryQueryParameters queryParameters);
        Task<CategoryDto> GetCategoryById(string id);

        #endregion

        #region lendConfig

        Task<LendConfigDto> CreateLendConfigAsync(LendConfigCreateDto body);
        Task DeleteLendConfigAsync(string id);
        Task UpdateLendConfigAsync(string id, LendConfigCreateDto updateDto);
        Task<List<LendConfigDto>> GetLendConfigsAsync();
        Task<LendConfigDto> GetLendConfigById(string id);

        #endregion

        #region lendRecord

        Task<LendRecordDto> CreateLendRecordAsync(LendRecordCreateDto body);
        Task DeleteLendRecordAsync(string id);
        Task<List<LendRecordDto>> GetLendRecordsAsync(LendRecordQueryParameters queryParameters);
        Task<LendRecordDto> GetLendRecordById(string id);
        Task ReturnBookAsync(string id);

        #endregion

        #region notice

        Task<NoticeDto> CreateNoticeAsync(NoticeCreateDto body);
        Task DeleteNoticeAsync(string id);
        Task UpdateNoticeAsync(string id, NoticeCreateDto updateDto);
        Task<List<NoticeDto>> GetNoticesAsync(QueryParameters queryParameters);
        Task<NoticeDto> GetNoticeById(string id);

        #endregion
    }
}