using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Library.API.Entities;
using Library.API.Repository.BASE;
using Library.API.Repository.Interface;
using Library.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.Repository;

public class LendRecordRepository : BaseRepository<LendRecord, Guid>, ILendRecordRepository
{
    public LendRecordRepository(LibraryDbContext dbContext) : base(dbContext)
    {
    }

    public Task<List<ChartDataItem>> SelectLast30DaysCountAsync()
    {
        return Task.FromResult(Table.Where(record =>
                record.StartTime > DateTime.Now.Date.AddDays(-30) && record.StartTime < DateTime.Now.Date)
            .GroupBy(i => i.StartTime.Date.ToString())
            .Select(i => new ChartDataItem {X = i.Key, Y = i.Count()}).ToList());
    }

    public async Task<List<ChartDataItem>> SelectOneYearCountAsync()
    {
        var dt = DateTime.Now;
        var thisMonth = dt.AddDays(-(dt.Day) + 1).Date;
        return await Table
            .Where(record => record.StartTime > thisMonth.AddYears(-1) && record.StartTime < thisMonth)
            .GroupBy(i => new {Year = i.StartTime.Year, Month = i.StartTime.Month})
            .OrderBy(key => key.Key.Year).ThenBy(key => key.Key.Month)
            .Select(i => new ChartDataItem {X = i.Key.Year + "-" + i.Key.Month, Y = i.Count()})
            .ToListAsync();
    }
}