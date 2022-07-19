using AutoMapper;
using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class LendConfigService : BaseHttpService, ILendConfigService
{
    public LendConfigService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client,
        localStorage)
    {
        _client = client;
        _mapper = mapper;
    }

    public async Task<Response<List<LendConfigDto>>> GetAsync()
    {
        Response<List<LendConfigDto>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetLendConfigsAsync();
            response = new Response<List<LendConfigDto>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<LendConfigDto>>(exception);
        }

        return response;
    }

    public async Task<Response<LendConfigDto>> GetAsync(string id)
    {
        Response<LendConfigDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetLendConfigById(id);
            response = new Response<LendConfigDto>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<LendConfigDto>(exception);
        }

        return response;
    }

    public async Task<Response<int>> CreateAsync(LendConfigCreateDto lendConfig)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            await _client.CreateLendConfigAsync(lendConfig);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> EditAsync(string id, LendConfigCreateDto updateDto)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            response.Success = await _client.UpdateLendConfigAsync(id, updateDto);
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<int>(exception);
        }

        return response;
    }

    public async Task<Response<int>> DeleteAsync(string id)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.DeleteLendConfigAsync(id);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    #region field

    private readonly IClient _client;
    private readonly IMapper _mapper;

    #endregion
}