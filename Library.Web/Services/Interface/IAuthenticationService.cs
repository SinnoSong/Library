using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface IAuthenticationService
{
    Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginUser);

    Task Logout();

    Task<Response<bool>> RegisterAsync(RegisterUser registerUser);
}