using Library.API.Entities;
using Library.API.Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Library.API.Repository.Interface;
using Library.Common.Models;

namespace Library.API.Service
{
    public class LendRecordService : BaseService<LendRecord, Guid>, ILendRecordService
    {
        private readonly ILendRecordRepository _dal;

        public LendRecordService(ILendRecordRepository dal)
        {
            _dal = dal;
            BaseDal = dal;
        }

        public async Task<List<ChartDataItem>> SelectLast30DaysCountAsync()
        {
            return await _dal.SelectLast30DaysCountAsync();
        }

        public async Task<List<ChartDataItem>> SelectOneYearCountAsync()
        {
            return await _dal.SelectOneYearCountAsync();
        }
    }
}