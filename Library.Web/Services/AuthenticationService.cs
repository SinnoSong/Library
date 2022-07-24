using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Providers;
using Library.Web.Services.Interface;
using Microsoft.AspNetCore.Components.Authorization;

namespace Library.Web.Services;

public class AuthenticationService : BaseHttpService, IAuthenticationService
{
    public AuthenticationService(IClient client, ILocalStorageService localStorage,
        AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
    {
        _httpClient = client;
        _localStorage = localStorage;
        _authenticationStateProvider = (authenticationStateProvider as ApiAuthenticationStateProvider)!;
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
                Success = true
            };
            await _localStorage.SetItemAsStringAsync("accessToken", result.Token);
            await _localStorage.SetItemAsStringAsync("userId", response.Data.UserId);
            await _localStorage.SetItemAsStringAsync("email", response.Data.Email);
            await _localStorage.SetItemAsync("menuPath", response.Data.MenuPath);

            await _authenticationStateProvider.LoggedIn();
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<AuthResponse>(exception);
        }

        return response;
    }

    public async Task Logout()
    {
        await _localStorage.ClearAsync();
        await _authenticationStateProvider.LoggedOut();
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

    #region field

    private readonly IClient _httpClient;
    private readonly ILocalStorageService _localStorage;
    private readonly ApiAuthenticationStateProvider _authenticationStateProvider;

    #endregion
}