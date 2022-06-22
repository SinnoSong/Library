﻿using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface
{
    public interface ILendRecordService
    {
        Task<Response<List<LendRecordDto>>> GetAsync(QueryParameters queryParameters);
        Task<Response<LendRecordDto>> GetAsync(string id);
        Task<Response<int>> CreateAsync(LendRecordCreateDto bookCreateDto);
        Task<Response<int>> EditAsync(string id, LendRecordCreateDto bookUpdateDto);
        Task<Response<int>> DeleteAsync(string id);
    }
}
