using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface IBookService
{
    Task<Response<List<BookDto>>> GetAsync(BookQueryParameters? queryParameters);
    Task<Response<BookDto>> GetAsync(string id);
    Task<Response<int>> CreateAsync(BookCreateDto bookCreateDto);
    Task<Response<int>> EditAsync(string id, BookUpdateDto bookUpdateDto);
    Task<Response<int>> DeleteAsync(string id);
}