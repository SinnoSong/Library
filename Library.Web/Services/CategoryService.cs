using AutoMapper;
using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services
{
    public class CategoryService : BaseHttpService, ICategoryService
    {
        #region field
        private readonly IClient _client;
        private readonly IMapper _mapper;
        #endregion

        public CategoryService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateAsync(CategoryCreateDto categoryCreateDto)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await _client.CreateCategoryAsync(categoryCreateDto);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public Task<Response<int>> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<int>> EditAsync(string id, CategoryCreateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<CategoryDto>>> GetAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<Response<CategoryDto>> GetAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
