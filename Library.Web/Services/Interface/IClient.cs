using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface IClient
    {
        public HttpClient HttpClient { get; }

        // todo 接口

        Task<AuthResponse> LoginAsync(LoginUserDto body);
        Task<bool> RegisterUserAsync(RegisterUser registerUser);

        #region book

        Task<BookCreateDto> CreateBookAsync(BookCreateDto body);
        Task DeleteBookAsync(string id);
        Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto);
        Task<List<BookDto>> GetBooksAsync(BookQueryParameters queryParameters);
        Task<BookDto> GetBookById(string id);

        #endregion

        #region category

        Task<CategoryCreateDto> CreateCategoryAsync(CategoryCreateDto body);
        Task DeleteCategoryAsync(string id);
        Task UpdateCategoryAsync(string id, CategoryCreateDto bookUpdateDto);
        Task<List<CategoryDto>> GetCategoriesAsync(QueryParameters queryParameters);
        Task<CategoryDto> GetCategoryById(string id);

        #endregion

        #region lendConfig

        Task<LendConfigDto> CreateLendConfigAsync(LendConfigCreateDto body);
        Task DeleteLendConfigAsync(string id);
        Task UpdateLendConfigAsync(string id, LendConfigCreateDto bookUpdateDto);
        Task<List<LendConfigDto>> GetLendConfigsAsync(QueryParameters queryParameters);
        Task<LendConfigDto> GetLendConfigById(string id);

        #endregion

        #region lendRecord

        Task<LendRecordDto> CreateLendRecordAsync(LendRecordCreateDto body);
        Task DeleteLendRecordAsync(string id);
        Task UpdateLendRecordAsync(string id, LendRecordCreateDto bookUpdateDto);
        Task<List<LendRecordDto>> GetLendRecordsAsync(QueryParameters queryParameters);
        Task<LendRecordDto> GetLendRecordById(string id);

        #endregion

        #region notice

        Task<NoticeDto> CreateNoticeAsync(NoticeCreateDto body);
        Task DeleteNoticeAsync(string id);
        Task UpdateNoticeAsync(string id, NoticeCreateDto bookUpdateDto);
        Task<List<NoticeDto>> GetNoticesAsync(QueryParameters queryParameters);
        Task<NoticeDto> GetNoticeById(string id);

        #endregion
    }
}