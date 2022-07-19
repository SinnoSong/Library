using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface INoticeService
{
    Task<Response<List<NoticeNoContentVo>>> GetAsync(QueryParameters queryParameters);
    Task<Response<NoticeDto>> GetAsync(string id);
    Task<Response<int>> CreateAsync(NoticeCreateDto createDto);
    Task<Response<int>> EditAsync(string id, NoticeCreateDto updateDto);
    Task<Response<int>> DeleteAsync(string id);
}