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

        public CategoryService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client,
            localStorage)
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

        public async Task<Response<int>> DeleteAsync(string id)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await _client.DeleteCategoryAsync(id);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }

            return response;
        }

        public async Task<Response<int>> EditAsync(string id, CategoryCreateDto categoryUpdateDto)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await _client.UpdateCategoryAsync(id, categoryUpdateDto);
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

        public async Task<Response<List<CategoryDto>>> GetAsync(CategoryQueryParameters queryParameters)
        {
            Response<List<CategoryDto>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.GetCategoriesAsync(queryParameters);
                response = new Response<List<CategoryDto>>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<CategoryDto>>(exception);
            }

            return response;
        }

        public async Task<Response<CategoryDto>> GetAsync(string id)
        {
            Response<CategoryDto> response;

            try
            {
                await GetBearerToken();
                var data = await _client.GetCategoryById(id);
                response = new Response<CategoryDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<CategoryDto>(exception);
            }

            return response;
        }
    }
}