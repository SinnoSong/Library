using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services
{
    public class Client : IClient
    {
        #region field

        private readonly HttpClient _httpClient;
        private Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

        public HttpClient HttpClient
        {
            get { return _httpClient; }
        }

        public bool ReadResponseAsString { get; set; }

        protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings
        {
            get { return _settings.Value; }
        }

        #endregion


        #region ctor

        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _settings = new Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings);
        }

        #endregion


        private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            return settings;
        }

        public Task<AuthResponse> LoginAsync(LoginUserDto body)
        {
            return LoginAsync(body, CancellationToken.None);
        }

        public Task<bool> RegisterUserAsync(RegisterUser registerUser)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<AuthResponse> LoginAsync(LoginUserDto body, CancellationToken cancellationToken)
        {
            // todo 需要重写调用接口
            var urlBuilder_ = new System.Text.StringBuilder();
            urlBuilder_.Append("api/Auth/login");

            var client_ = _httpClient;
            var disposeClient_ = false;
            try
            {
                using (var request_ = new HttpRequestMessage())
                {
                    var content_ =
                        new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(body, _settings.Value));
                    content_.Headers.ContentType =
                        System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    request_.Content = content_;
                    request_.Method = new HttpMethod("POST");
                    request_.Headers.Accept.Add(
                        System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("text/plain"));


                    var url_ = urlBuilder_.ToString();
                    request_.RequestUri = new Uri(url_, UriKind.RelativeOrAbsolute);


                    var response_ = await client_
                        .SendAsync(request_, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                        .ConfigureAwait(false);
                    var disposeResponse_ = true;
                    try
                    {
                        var headers_ = Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
                        if (response_.Content != null && response_.Content.Headers != null)
                        {
                            foreach (var item_ in response_.Content.Headers)
                                headers_[item_.Key] = item_.Value;
                        }


                        var status_ = (int) response_.StatusCode;
                        if (status_ == 200)
                        {
                            var objectResponse_ =
                                await ReadObjectResponseAsync<AuthResponse>(response_, headers_, cancellationToken)
                                    .ConfigureAwait(false);
                            if (objectResponse_.Object == null)
                            {
                                throw new ApiException("Response was null which was not expected.", status_,
                                    objectResponse_.Text, headers_, null);
                            }

                            return objectResponse_.Object;
                        }
                        else
                        {
                            var responseData_ = response_.Content == null
                                ? null
                                : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
                            throw new ApiException(
                                "The HTTP status code of the response was not expected (" + status_ + ").", status_,
                                responseData_, headers_, null);
                        }
                    }
                    finally
                    {
                        if (disposeResponse_)
                            response_.Dispose();
                    }
                }
            }
            finally
            {
                if (disposeClient_)
                    client_.Dispose();
            }
        }

        #region Book

        // todo 接口未实现
        public Task<BookCreateDto> CreateBookAsync(BookCreateDto body)
        {
            throw new NotImplementedException();
        }

        public Task DeleteBookAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBookAsync(string id, BookUpdateDto bookUpdateDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<BookDto>> GetBooksAsync(QueryParameters queryParameters)
        {
            throw new NotImplementedException();
        }

        public Task<BookDto> GetBookById(string id)
        {
            throw new NotImplementedException();
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

        public async Task<LendConfigDto> CreateLendConfigAsync(LendConfigDto body)
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

        public Task<LendRecordDto> CreateLendRecordAsync(LendRecordDto body)
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

        public Task<NoticeDto> CreateNoticeAsync(NoticeDto body)
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

        protected virtual async Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(HttpResponseMessage response,
            IReadOnlyDictionary<string, IEnumerable<string>> headers, CancellationToken cancellationToken)
        {
            if (ReadResponseAsString)
            {
                var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                try
                {
                    var typedBody =
                        Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
                    return new ObjectResponseResult<T>(typedBody, responseText);
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int) response.StatusCode, responseText, headers, exception);
                }
            }
            else
            {
                try
                {
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var streamReader = new StreamReader(responseStream))
                    using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
                    {
                        var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
                        var typedBody = serializer.Deserialize<T>(jsonTextReader);
                        return new ObjectResponseResult<T>(typedBody, string.Empty);
                    }
                }
                catch (Newtonsoft.Json.JsonException exception)
                {
                    var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
                    throw new ApiException(message, (int) response.StatusCode, string.Empty, headers, exception);
                }
            }
        }


        protected struct ObjectResponseResult<T>
        {
            public ObjectResponseResult(T responseObject, string responseText)
            {
                Object = responseObject;
                Text = responseText;
            }

            public T Object { get; }

            public string Text { get; }
        }
    }
}