using System;
using Library.API.Entities;
using Library.API.Repository.Interface;
using Library.API.Service.Interface;

namespace Library.API.Service
{
    public class NoticeService : BaseService<Notice, Guid>, INoticeService
    {
        public NoticeService(IBaseRepository<Notice, Guid> dal)
        {
            BaseDal = dal;
        }
    }
}
