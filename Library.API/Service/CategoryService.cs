using Library.API.Entities;
using Library.API.Repository.Interface;
using Library.API.Service.Interface;
using System;

namespace Library.API.Service
{
    public class CategoryService : BaseService<Category, Guid>, ICategoryService
    {
        public CategoryService(IBaseRepository<Category, Guid> dal)
        {
            BaseDal = dal;
        }
    }
}
