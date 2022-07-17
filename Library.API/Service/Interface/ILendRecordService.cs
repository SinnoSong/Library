using Library.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Common.Models;

namespace Library.API.Service.Interface
{
    public interface ILendRecordService : IBaseService<LendRecord, Guid>
    {
        Task<List<ChartDataItem>> SelectLast30DaysCountAsync();

        Task<List<ChartDataItem>> SelectOneYearCountAsync();
    }
}