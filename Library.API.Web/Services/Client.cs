using Library.API.Web.Services.Interface;

namespace Library.API.Web.Services
{
    public class Client : IClient
    {
        private readonly HttpClient _httpClient;
        public Client(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public HttpClient HttpClient { get { return _httpClient; } }
    }
}
