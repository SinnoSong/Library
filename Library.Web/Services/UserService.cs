using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class UserService : BaseHttpService, IUserService
{
    private readonly IClient _client;

    public UserService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
    {
        _client = client;
    }

    public async Task<Response<int>> UpdateUserGrade(Guid userId, byte grade)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.UpdateUserGrade(userId, grade);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> ChangeEmail(Guid userId, string newEmail)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.ChangeEmail(userId, newEmail);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> ChangePassword(Guid id, UserPasswordChangeDto userPasswordChangeDto)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.ChangePassword(id, userPasswordChangeDto);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> AddAdministrator(UserDto userDto)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.AddAdministrator(userDto);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<List<UserDto>>> GetUsers(UserQueryParameters parameters)
    {
        Response<List<UserDto>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetUsers(parameters);
            response = new Response<List<UserDto>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<UserDto>>(exception);
        }

        return response;
    }

    #region field

    #endregion
}