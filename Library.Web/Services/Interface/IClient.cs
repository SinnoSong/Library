using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface IClient
    {
        public HttpClient HttpClient { get; }

        // todo 接口

        Task<AuthResponse> LoginAsync(LoginUserDto body);

        Task<BookCreateDto> CreateBookAsync(BookCreateDto body);
        Task DeleteBookAsync(string id);
        Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto);
        Task<List<BookReadOnlyDto>> GetBooksAsync(QueryParameters queryParameters);
        Task<BookReadOnlyDto> GetBookById(string id);
    }
}
