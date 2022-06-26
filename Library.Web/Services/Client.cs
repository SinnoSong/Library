using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Constants;
using Library.Web.Helper;
using Library.Web.Models;
using Library.Web.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace Library.Web.Services
{
    public class Client : IClient
    {
        #region field

        private const string ApplicationUrl = "http://localhost:5000/";
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


        #region 封装请求

        private async Task<T> SendRequest<T>(HttpMethod method, string queryUrl, string? accessToken = null,
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
            if (accessToken != null)
            {
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }

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

        private async Task<bool> SendRequest(HttpMethod method, string queryUrl, string? accessToken = null,
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
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
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


        #region auth

        public async Task<AuthResponse> LoginAsync(LoginUserDto body)
        {
            var apiUrl = ApplicationUrl + Apis.AuthLogin;
            return await SendRequest<AuthResponse>(HttpMethod.Post, apiUrl, json: JsonConvert.SerializeObject(body));
        }

        public async Task<bool> RegisterUserAsync(RegisterUser registerUser)
        {
            var apiUrl = ApplicationUrl + Apis.AuthRegister;
            return await SendRequest(HttpMethod.Post, apiUrl, json: JsonConvert.SerializeObject(registerUser));
        }

        #endregion

        #region Book

        public async Task<BookCreateDto> CreateBookAsync(BookCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateBook;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<BookCreateDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
        }

        public async Task DeleteBookAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(bookUpdateDto));
        }

        public async Task<List<BookDto>> GetBooksAsync(BookQueryParameters? queryParameters)
        {
            var apiUrl = ApplicationUrl + Apis.CreateBook;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            var dict = queryParameters.ToDictionary();
            return await SendRequest<List<BookDto>>(HttpMethod.Post, apiUrl, accessToken, queryPairs: dict);
        }

        public async Task<BookDto> GetBookById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetBook + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<BookDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion

        #region Category

        public async Task<CategoryCreateDto> CreateCategoryAsync(CategoryCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateCategory;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<CategoryCreateDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
        }

        public async Task DeleteCategoryAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetCategory + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateCategoryAsync(string id, CategoryCreateDto updateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetCategory + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(updateDto));
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync(CategoryQueryParameters queryParameters)
        {
            var apiUrl = ApplicationUrl + Apis.CreateCategory;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            var dict = queryParameters.ToDictionary();
            return await SendRequest<List<CategoryDto>>(HttpMethod.Post, apiUrl, accessToken, queryPairs: dict);
        }

        public async Task<CategoryDto> GetCategoryById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetCategory + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<CategoryDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion

        #region lendConfig

        public async Task<LendConfigDto> CreateLendConfigAsync(LendConfigCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateLendConfig;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<LendConfigDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
        }

        public async Task DeleteLendConfigAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendConfig + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateLendConfigAsync(string id, LendConfigCreateDto updateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendConfig + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(updateDto));
        }

        public async Task<List<LendConfigDto>> GetLendConfigsAsync()
        {
            var apiUrl = ApplicationUrl + Apis.CreateLendConfig;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<List<LendConfigDto>>(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task<LendConfigDto> GetLendConfigById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendConfig + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<LendConfigDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion

        #region lendRecord

        public async Task<LendRecordDto> CreateLendRecordAsync(LendRecordCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateLendRecord;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<LendRecordDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
        }

        public async Task DeleteLendRecordAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendRecord + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateLendRecordAsync(string id, LendRecordCreateDto updateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendRecord + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(updateDto));
        }

        public async Task<List<LendRecordDto>> GetLendRecordsAsync(LendRecordQueryParameters queryParameters)
        {
            var apiUrl = ApplicationUrl + Apis.CreateLendRecord;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            var dict = queryParameters.ToDictionary();
            return await SendRequest<List<LendRecordDto>>(HttpMethod.Post, apiUrl, accessToken, queryPairs: dict);
        }

        public async Task<LendRecordDto> GetLendRecordById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetLendRecord + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<LendRecordDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion

        #region notice

        public async Task<NoticeDto> CreateNoticeAsync(NoticeCreateDto body)
        {
            var apiUrl = ApplicationUrl + Apis.CreateNotice;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<NoticeDto>(HttpMethod.Post, apiUrl, accessToken,
                JsonConvert.SerializeObject(body));
        }

        public async Task DeleteNoticeAsync(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetNotice + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken);
        }

        public async Task UpdateNoticeAsync(string id, NoticeCreateDto updateDto)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetNotice + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            await SendRequest(HttpMethod.Post, apiUrl, accessToken, JsonConvert.SerializeObject(updateDto));
        }

        public async Task<List<NoticeDto>> GetNoticesAsync(QueryParameters queryParameters)
        {
            var apiUrl = ApplicationUrl + Apis.CreateNotice;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            var dict = queryParameters.ToDictionary();
            return await SendRequest<List<NoticeDto>>(HttpMethod.Post, apiUrl, accessToken, queryPairs: dict);
        }

        public async Task<NoticeDto> GetNoticeById(string id)
        {
            var apiUrl = ApplicationUrl + Apis.DeleteOrUpdateOrGetNotice + id;
            var accessToken = await _localStorageService.GetAccessTokenAsync();
            return await SendRequest<NoticeDto>(HttpMethod.Post, apiUrl, accessToken);
        }

        #endregion
    }
}