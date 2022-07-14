using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface ILendRecordService
    {
        Task<Response<List<LendRecordDto>>> GetAsync(LendRecordQueryParameters queryParameters);
        Task<Response<LendRecordDto>> GetAsync(string id);
        Task<Response<int>> CreateAsync(LendRecordCreateDto bookCreateDto);
        Task<Response<int>> DeleteAsync(string id);
        Task<Response<int>> PutAsync(string id);
        Task<Response<int>> RenewAsync(string id);
    }
}