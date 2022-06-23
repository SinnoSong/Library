using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Providers;
using Library.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace Library.Web.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        #region field

        private readonly IClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        #endregion

        public AuthenticationService(IClient client, ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            _httpClient = client;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginUser)
        {
            Response<AuthResponse> response;
            try
            {
                var result = await _httpClient.LoginAsync(loginUser);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true,
                };
                await _localStorage.SetItemAsync("accessToken", result.Token);

                await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedIn();
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthResponse>(exception);
            }

            return response;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider) _authenticationStateProvider).LoggedOut();
        }

        public async Task<Response<bool>> RegisterAsync(RegisterUser registerUser)
        {
            Response<bool> response;
            try
            {
                var result = await _httpClient.RegisterUserAsync(registerUser);
                response = new Response<bool>
                {
                    Data = result,
                    Success = true
                };
            }
            catch (ApiException e)
            {
                response = ConvertApiException<bool>(e);
            }

            return response;
        }
    }
}