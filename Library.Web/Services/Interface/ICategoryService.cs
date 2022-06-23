using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDto>>> GetAsync(CategoryQueryParameters queryParameters);
        Task<Response<CategoryDto>> GetAsync(string id);
        Task<Response<int>> CreateAsync(CategoryCreateDto categoryCreateDto);
        Task<Response<int>> EditAsync(string id, CategoryCreateDto categoryUpdateDto);
        Task<Response<int>> DeleteAsync(string id);
    }
}
