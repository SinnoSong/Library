using System;
using Library.API.Entities;
using Library.API.Repository.Interface;
using Library.API.Service.Interface;

namespace Library.API.Service
{
    public class LendRecordService : BaseService<LendRecord, Guid>, ILendRecordService
    {
        public LendRecordService(IBaseRepository<LendRecord, Guid> bal)
        {
            BaseDal = bal;
        }
    }
}
