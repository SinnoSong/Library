using Library.API.Web.Models;

namespace Library.API.Web.Services.Interface
{
    public interface IClient
    {
        public HttpClient HttpClient { get; }

        // todo 接口

        Task<AuthResponse> LoginAsync(LoginUserDto body);
    }
}
