using System.Net.Http.Headers;
using Blazored.LocalStorage;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class BaseHttpService
{
    #region ctor

    protected BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        _client = client;
        _localStorage = localStorage;
    }

    #endregion

    protected static Response<TGuid> ConvertApiException<TGuid>(ApiException apiException)
    {
        if (apiException.StatusCode == 400)
            return new Response<TGuid>
            {
                Message = "数据校验失败！",
                ValidationErrors = apiException.Response,
                Success = false
            };

        if (apiException.StatusCode == 404) return new Response<TGuid> {Message = "没有找到请求内容，请稍后重试！", Success = false};

        if (apiException.StatusCode == 401) return new Response<TGuid> {Message = "权限校验失败，请稍后重试！", Success = false};

        if (apiException.StatusCode is >= 200 and <= 299)
            return new Response<TGuid> {Message = "Operation Reported Success", Success = true};

        return new Response<TGuid> {Message = apiException.Message, Success = false};
    }

    protected async Task GetBearerToken()
    {
        var token = await _localStorage.GetItemAsync<string>("accessToken");
        if (token != null)
            _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    #region field

    private readonly IClient _client;
    private readonly ILocalStorageService _localStorage;

    #endregion
}