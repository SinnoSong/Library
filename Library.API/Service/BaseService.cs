using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.API.Repository.Interface;
using Library.API.Service.Interface;

namespace Library.API.Service
{
    public abstract class BaseService<T, TId> : IBaseService<T, TId>
    {
        /// <summary>
        /// 通过在子类的构造函数中注入，这里是基类，不用构造函数
        /// </summary>
        public IBaseRepository<T, TId> BaseDal { get; set; }
        public async Task<T> AddAsync(T entity)
        {
            return await BaseDal.AddAsync(entity);
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await BaseDal.AddAsync(entities);
        }

        public async Task<int> CountAsync()
        {
            return await BaseDal.CountAsync();
        }

        public async Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await BaseDal.CountByConditionAsync(expression);
        }

        public async Task DeleteAsync(T entity)
        {
            await BaseDal.DeleteAsync(entity);
        }

        public async Task DeleteByConditionAsync(Expression<Func<T, bool>> expression)
        {
            await BaseDal.DeleteByConditionAsync(expression);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return await BaseDal.GetAllAsync();
        }

        public async Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await BaseDal.GetByConditionAsync(expression);
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await BaseDal.GetByIdAsync(id);
        }

        public async Task<bool> IsExistAsync(TId id)
        {
            return await BaseDal.IsExistAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            return await BaseDal.UpdateAsync(entity);
        }

        public async Task<bool> SaveAsync()
        {
            return await BaseDal.SaveAsync();
        }
    }
}
