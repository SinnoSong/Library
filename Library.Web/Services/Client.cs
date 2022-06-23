using System.Text;
using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Constants;
using Library.Web.Helper;
using Library.Web.Models;
using Library.Web.Services.Interface;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Library.Web.Services
{
    public class Client : IClient
    {
        #region field

        private const string ApplicationUrl = "http://localhost:52766/";
        public HttpClient HttpClient { get; }

        private readonly ILocalStorageService _localStorageService;

        #endregion


        #region ctor

        public Client(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            HttpClient = httpClient;
            _localStorageService = localStorageService;
        }

        #endregion

        public Task<bool> RegisterUserAsync(RegisterUser registerUser)
        {
            throw new NotImplementedException();
        }

        #region 封装请求

        private async Task<T> SendRequest<T>(HttpMethod method, string queryUrl, string accessToken,
            string? json = null, Dictionary<string, object>? queryPairs = null)
        {
            if (queryPairs != null)
            {
                var queryParams = new StringBuilder("?");
                foreach (var (key, value) in queryPairs)
                {
                    queryParams.Append(key + "=" + value + "&");
                }

                queryParams.Remove(queryParams.Length - 1, 1);
                queryUrl += queryParams.ToString();
            }

            using var request = new HttpRequestMessage(method, queryUrl);
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            if (json != null)
            {
                var content = new StringContent(json);
                request.Content = content;
                content.Headers.ContentType =
                    System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                request.Method = HttpMethod.Post;
                request.Headers.Accept.Add(
                    System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
            }

            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            foreach (var item in response.Content.Headers)
                headers[item.Key] = item.Value;

            var status = (int)response.StatusCode;
            if (status == 200)
            {
                var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var objectResponse = JsonConvert.DeserializeObject<T>(stringContent);
                if (objectResponse == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, stringContent, headers);
                }

                return objectResponse;
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").",
                    status, responseData, headers);
            }
        }

        private async Task<bool> SendRequest(HttpMethod method, string queryUrl, string accessToken,
            string? json = null, Dictionary<string, object>? queryPairs = null)
        {
            if (queryPairs != null)
            {
                var queryParams = new StringBuilder("?");
                foreach (var (key, value) in queryPairs)
                {
                    queryParams.Append(key + "=" + value + "&");
                }

                queryParams.Remove(queryParams.Length - 1, 1);
                queryUrl += queryParams.ToString();
            }

            using var request = new HttpRequestMessage(method, queryUrl);
            request.Headers.Add("Authorization", "Bearer " + accessToken);
            if (json != null)
            {
                var content = new StringContent(json);
                request.Content = content;
                content.Headers.ContentType =
                    System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                request.Method = HttpMethod.Post;
                request.Headers.Accept.Add(
                    System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
            }

            using var response = await HttpClient.SendAsync(request).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        #endregion

        public async Task<AuthResponse> LoginAsync(LoginUserDto body)
        {
            // todo 需要重写调用接口
            using var request = new HttpRequestMessage();
            var content = new StringContent(JsonConvert.SerializeObject(body));
            request.Content = content;
            content.Headers.ContentType =
                System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            request.Method = HttpMethod.Post;
            request.Headers.Accept.Add(
                System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
            request.RequestUri = new Uri(ApplicationUrl + Apis.AuthLogin, UriKind.RelativeOrAbsolute);

            var response = await HttpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            var headers = response.Headers.ToDictionary(h => h.Key, h => h.Value);
            foreach (var item in response.Content.Headers)
                headers[item.Key] = item.Value;

            var status = (int)response.StatusCode;
            if (status == 200)
            {
                var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var authResponse = JsonConvert.DeserializeObject<AuthResponse>(stringContent);
                if (authResponse == null)
                {
                    throw new ApiException("Response was null which was not expected.", status, stringContent,
                        headers);
                }

                return authResponse;
            }
            else
            {
                var responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ApiException(
                    "The HTTP status code of the response was not expected (" + status + ").", status,
                    responseData, headers);
            }
        }

        #region Book

        // todo 接口未实现
        public async Task<BookCreateDto> CreateBookAsync(BookCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateBook;
            var accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");
            var bookCreateDto = await SendRequest<BookCreateDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
            return bookCreateDto;
        }

        public async Task DeleteBookAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(bookUpdateDto));
        }

        public async Task<List<BookDto>> GetBooksAsync(BookQueryParameters queryParameters)
        {
            var apiUrl = ApplicationUrl + Apis.CreateBook;
            var accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");
            var dict = queryParameters.ToDictionary();
            return await SendRequest<List<BookDto>>(HttpMethod.Post, apiUrl, accessToken, null, dict);
        }

        public async Task<BookDto> GetBookById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetItemAsStringAsync("accessToken");
            return await SendRequest<BookDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion

        #region Category

        public Task<CategoryCreateDto> CreateCategoryAsync(CategoryCreateDto body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(string id, CategoryCreateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<CategoryDto>> GetCategoriesAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryDto> GetCategoryById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region lendConfig

        public Task<LendConfigDto> CreateLendConfigAsync(LendConfigCreateDto body)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteLendConfigAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLendConfigAsync(string id, LendConfigCreateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LendConfigDto>> GetLendConfigsAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public async Task<LendConfigDto> GetLendConfigById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region lendRecord

        public Task<LendRecordDto> CreateLendRecordAsync(LendRecordCreateDto body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLendRecordAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateLendRecordAsync(string id, LendRecordCreateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<LendRecordDto>> GetLendRecordsAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<LendRecordDto> GetLendRecordById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region notice

        public Task<NoticeDto> CreateNoticeAsync(NoticeCreateDto body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteNoticeAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateNoticeAsync(string id, NoticeCreateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<NoticeDto>> GetNoticesAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<NoticeDto> GetNoticeById(string id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}