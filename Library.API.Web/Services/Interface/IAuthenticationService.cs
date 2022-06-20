using Library.API.Web.Models;

namespace Library.API.Web.Services.Interface
{
    public interface IAuthenticationService
    {
        Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginUser);

        public Task Logout();
    }
}
