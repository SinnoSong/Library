using System.Collections.Generic;
using System.Threading.Tasks;
using Library.API.Service.Interface;
using Library.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Administrator,SuperAdministrator")]
public class DashBoardController : Controller
{
    #region field

    private readonly ILendRecordService _lendRecordService;

    #endregion

    #region ctor

    public DashBoardController(IServicesWrapper servicesWrapper)
    {
        _lendRecordService = servicesWrapper.LendRecord;
    }

    #endregion

    [HttpGet("month")]
    public async Task<ActionResult<List<ChartDataItem>>> SelectLast30DaysData()
    {
        return await _lendRecordService.SelectLast30DaysCountAsync();
    }

    [HttpGet("year")]
    public async Task<ActionResult<List<ChartDataItem>>> SelectLastYearData()
    {
        return await _lendRecordService.SelectOneYearCountAsync();
    }
}