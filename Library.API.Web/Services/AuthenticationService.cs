using Blazored.LocalStorage;
using Library.API.Web.Models;
using Library.API.Web.Providers;
using Library.API.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace Library.API.Web.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        #region field
        private readonly IClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        #endregion

        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            httpClient = client;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<Response<AuthResponse>> AuthenticateAsync(LoginUserDto loginUser)
        {
            Response<AuthResponse> response;
            try
            {
                var result = await httpClient.LoginAsync(loginUser);
                response = new Response<AuthResponse>
                {
                    Data = result,
                    Success = true,
                };
                //Store Token
                await localStorage.SetItemAsync("accessToken", result.Token);

                //Change auth state of app
                await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedIn();
            }
            catch (ApiException exception)
            {
                response = ConvertApiException<AuthResponse>(exception);
            }

            return response;
        }

        public async Task Logout()
        {
            await ((ApiAuthenticationStateProvider)authenticationStateProvider).LoggedOut();
        }
    }
}
