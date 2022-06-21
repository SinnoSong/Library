using AutoMapper;
using Blazored.LocalStorage;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        #region field
        private readonly IClient client;
        private readonly IMapper mapper;
        #endregion

        #region ctor
        public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client, localStorage)
        {
            this.client = client;
            this.mapper = mapper;
        }
        #endregion


        public async Task<Response<int>> CreateAsync(BookCreateDto bookCreateDto)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await client.CreateBookAsync(bookCreateDto);
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
                await client.DeleteBookAsync(id);
            }
            catch (ApiException e)
            {
                response = ConvertApiException<int>(e);
            }
            return response;
        }

        public async Task<Response<int>> EditAsync(string id, BookUpdateDto bookUpdateDto)
        {
            Response<int> response = new();

            try
            {
                await GetBearerToken();
                await client.UpdateBookAsync(id, bookUpdateDto);
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

        public async Task<Response<List<BookReadOnlyDto>>> GetAsync(QueryParameters queryParameters)
        {
            Response<List<BookReadOnlyDto>> response;

            try
            {
                await GetBearerToken();
                var data = await client.GetBooksAsync(queryParameters);
                response = new Response<List<BookReadOnlyDto>>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<BookReadOnlyDto>>(exception);
            }

            return response;
        }

        public async Task<Response<BookReadOnlyDto>> GetAsync(string id)
        {
            Response<BookReadOnlyDto> response;

            try
            {
                await GetBearerToken();
                var data = await client.GetBookById(id);
                response = new Response<BookReadOnlyDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<BookReadOnlyDto>(exception);
            }

            return response;
        }
    }
}
