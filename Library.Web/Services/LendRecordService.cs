using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class LendRecordService : BaseHttpService, ILendRecordService
{
    #region field

    private readonly IClient _client;

    #endregion

    public LendRecordService(IClient client, ILocalStorageService localStorage) : base(client,
        localStorage)
    {
        _client = client;
    }

    public async Task<Response<int>> CreateAsync(LendRecordCreateDto createDto)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            await _client.CreateLendRecordAsync(createDto);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> DeleteAsync(string id)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.DeleteLendRecordAsync(id);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> PutAsync(string id)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.UpdateLendRecordAsync(id);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> RenewAsync(string id)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            response.Success = await _client.UpdateLendRecordAsync(id);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<List<LendRecordDto>>> GetAsync(LendRecordQueryParameters queryParameters)
    {
        Response<List<LendRecordDto>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetLendRecordsAsync(queryParameters);
            response = new Response<List<LendRecordDto>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<LendRecordDto>>(exception);
        }

        return response;
    }

    public async Task<Response<LendRecordDto>> GetAsync(string id)
    {
        Response<LendRecordDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetLendRecordById(id);
            response = new Response<LendRecordDto>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<LendRecordDto>(exception);
        }

        return response;
    }
}