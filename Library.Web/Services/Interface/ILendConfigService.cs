using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface ILendConfigService
{
    Task<Response<List<LendConfigDto>>> GetAsync(QueryParameters queryParameters);
    Task<Response<LendConfigDto>> GetAsync(string id);
    Task<Response<int>> CreateAsync(LendConfigCreateDto bookCreateDto);
    Task<Response<int>> EditAsync(string id, BookUpdateDto bookUpdateDto);
    Task<Response<int>> DeleteAsync(string id);
}