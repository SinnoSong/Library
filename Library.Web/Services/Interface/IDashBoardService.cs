using Library.Common.Models;
using Library.Web.Models;

namespace Library.Web.Services.Interface;

public interface IDashBoardService
{
    Task<Response<List<ChartDataItem>>> SelectLast30DaysData();

    Task<Response<List<ChartDataItem>>> SelectLastYearData();
}