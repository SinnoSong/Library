using AutoMapper;
using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services
{
    public class BookService : BaseHttpService, IBookService
    {
        #region field

        private readonly IClient _client;
        private readonly IMapper _mapper;

        #endregion

        #region ctor

        public BookService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client,
            localStorage)
        {
            _client = client;
            _mapper = mapper;
        }

        #endregion


        public async Task<Response<int>> CreateAsync(BookCreateDto bookCreateDto)
        {
            var response = new Response<int>();
            try
            {
                await GetBearerToken();
                await _client.CreateBookAsync(bookCreateDto);
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
                await _client.DeleteBookAsync(id);
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
                await _client.UpdateBookAsync(id, bookUpdateDto);
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<int>(exception);
            }

            return response;
        }

        public async Task<Response<List<BookDto>>> GetAsync(BookQueryParameters queryParameters)
        {
            Response<List<BookDto>> response;

            try
            {
                await GetBearerToken();
                var data = await _client.GetBooksAsync(queryParameters);
                response = new Response<List<BookDto>>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<List<BookDto>>(exception);
            }

            return response;
        }

        public async Task<Response<BookDto>> GetAsync(string id)
        {
            Response<BookDto> response;

            try
            {
                await GetBearerToken();
                var data = await _client.GetBookById(id);
                response = new Response<BookDto>
                {
                    Data = data,
                    Success = true
                };
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<BookDto>(exception);
            }

            return response;
        }
    }
}