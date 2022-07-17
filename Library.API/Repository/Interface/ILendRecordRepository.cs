using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.API.Entities;
using Library.Common.Models;

namespace Library.API.Repository.Interface;

public interface ILendRecordRepository : IBaseRepository<LendRecord, Guid>
{
    Task<List<ChartDataItem>> SelectLast30DaysCountAsync();

    Task<List<ChartDataItem>> SelectOneYearCountAsync();
}