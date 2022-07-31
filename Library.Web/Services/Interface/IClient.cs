using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface IClient
{
    public HttpClient HttpClient { get; }

    Task<AuthResponse> LoginAsync(LoginUserDto body);
    Task<bool> RegisterUserAsync(RegisterUser registerUser);

    #region book

    Task<BookCreateDto> CreateBookAsync(BookCreateDto body);
    Task<bool> DeleteBookAsync(string id);
    Task<bool> UpdateBookAsync(string id, BookUpdateDto bookUpdateDto);
    Task<List<BookDto>> GetBooksAsync(BookQueryParameters? queryParameters);
    Task<BookDto> GetBookById(string id);

    #endregion

    #region category

    Task<CategoryCreateDto> CreateCategoryAsync(CategoryCreateDto body);
    Task<bool> DeleteCategoryAsync(string id);
    Task<bool> UpdateCategoryAsync(string id, CategoryCreateDto updateDto);
    Task<List<CategoryDto>> GetCategoriesAsync(CategoryQueryParameters queryParameters);
    Task<CategoryDto> GetCategoryById(string id);

    #endregion

    #region lendConfig

    Task<LendConfigDto> CreateLendConfigAsync(LendConfigCreateDto body);
    Task<bool> DeleteLendConfigAsync(string id);
    Task<bool> UpdateLendConfigAsync(string id, LendConfigCreateDto updateDto);
    Task<List<LendConfigDto>> GetLendConfigsAsync();
    Task<LendConfigDto> GetLendConfigById(string id);

    #endregion

    #region lendRecord

    Task<LendRecordDto> CreateLendRecordAsync(LendRecordCreateDto body);
    Task<bool> DeleteLendRecordAsync(string id);
    Task<List<LendRecordDto>> GetLendRecordsAsync(LendRecordQueryParameters queryParameters);
    Task<LendRecordDto> GetLendRecordById(string id);
    Task<bool> UpdateLendRecordAsync(string id);
    Task<bool> RenewAsync(string id);

    #endregion

    #region notice

    Task<NoticeDto> CreateNoticeAsync(NoticeCreateDto body);
    Task<bool> DeleteNoticeAsync(string id);
    Task<bool> UpdateNoticeAsync(string id, NoticeCreateDto updateDto);
    Task<List<NoticeNoContentVo>> GetNoticesAsync(QueryParameters queryParameters);
    Task<NoticeDto> GetNoticeById(string id);

    #endregion

    #region dashBoard

    Task<List<ChartDataItem>> SelectLast30DaysData();
    Task<List<ChartDataItem>> SelectLastYearData();

    #endregion

    #region user

    Task<bool> UpdateUserGrade(Guid userId, byte grade);

    Task<bool> ChangeEmail(Guid userId, string newEmail);

    Task<bool> ChangePassword(Guid id, UserPasswordChangeDto userPasswordChangeDto);

    Task<bool> AddAdministrator(RegisterAdmin userDto);

    Task<List<UserDto>> GetUsers(UserQueryParameters parameters);

    #endregion
}