using AutoMapper;
using Blazored.LocalStorage;
using Library.Common.Models;
using Library.Web.Models;
using Library.Web.Services.Interface;

namespace Library.Web.Services;

public class NoticeService : BaseHttpService, INoticeService
{
    #region ctor

    public NoticeService(IClient client, ILocalStorageService localStorage, IMapper mapper) : base(client,
        localStorage)
    {
        _client = client;
        _mapper = mapper;
    }

    #endregion

    public async Task<Response<int>> CreateAsync(NoticeCreateDto createDto)
    {
        var response = new Response<int>();
        try
        {
            await GetBearerToken();
            await _client.CreateNoticeAsync(createDto);
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
            response.Success = await _client.DeleteNoticeAsync(id);
        }
        catch (ApiException e)
        {
            response = ConvertApiException<int>(e);
        }

        return response;
    }

    public async Task<Response<int>> EditAsync(string id, NoticeCreateDto updateDto)
    {
        Response<int> response = new();

        try
        {
            await GetBearerToken();
            response.Success = await _client.UpdateNoticeAsync(id, updateDto);
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<int>(exception);
        }

        return response;
    }

    public async Task<Response<List<NoticeNoContentVo>>> GetAsync(QueryParameters queryParameters)
    {
        Response<List<NoticeNoContentVo>> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetNoticesAsync(queryParameters);
            response = new Response<List<NoticeNoContentVo>>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<List<NoticeNoContentVo>>(exception);
        }

        return response;
    }

    public async Task<Response<NoticeDto>> GetAsync(string id)
    {
        Response<NoticeDto> response;

        try
        {
            await GetBearerToken();
            var data = await _client.GetNoticeById(id);
            response = new Response<NoticeDto>
            {
                Data = data,
                Success = true
            };
        }
        catch (ApiException exception)
        {
            response = ConvertApiException<NoticeDto>(exception);
        }

        return response;
    }

    #region field

    private readonly IClient _client;
    private readonly IMapper _mapper;

    #endregion
}