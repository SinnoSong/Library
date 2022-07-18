using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class DashBoardService : BaseHttpService, IDashBoardService
{
    #region field

    private readonly IClient _client;

    #endregion

    public DashBoardService(IClient client, ILocalStorageService localStorage) : base(client, localStorage)
    {
        _client = client;
    }

    public async Task<Response<List<ChartDataItem>>> SelectLast30DaysData()
    {
        Response<List<ChartDataItem>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.SelectLast30DaysData();
            response = new Response<List<ChartDataItem>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<ChartDataItem>>(exception);
        }

        return response;
    }

    public async Task<Response<List<ChartDataItem>>> SelectLastYearData()
    {
        Response<List<ChartDataItem>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.SelectLastYearData();
            response = new Response<List<ChartDataItem>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<ChartDataItem>>(exception);
        }

        return response;
    }
}