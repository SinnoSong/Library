using Library.API.Entities;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Repository.BASE
{
    public class BaseRepository<T, TId> : IBaseRepository<T, TId> where T : class
    {
        #region fields
        private readonly LibraryDbContext _dbContext;
        public virtual DbSet<T> Table => _dbContext.Set<T>();
        #endregion

        #region ctor
        public BaseRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region protected methods

        protected async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 检查实体是否处于跟踪状态，如果是，则返回；如果不是，则添加跟踪状态
        /// </summary>
        /// <param name="entity"></param>
        protected virtual void AttachIfNot(T entity)
        {
            var entry = _dbContext.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        #endregion

        #region add

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await Table.AddRangeAsync(entities);
            await SaveAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            var entry = await Table.AddAsync(entity);
            await SaveAsync();
            return entry.Entity;
        }

        #endregion

        #region delete

        public async Task<bool> DeleteAsync(T entity)
        {
            AttachIfNot(entity);
            var entry = Table.Remove(entity);
            await SaveAsync();
            return entry.State == EntityState.Deleted;
        }

        public async Task DeleteByConditionAsync(Expression<Func<T, bool>> expression)
        {
            foreach (var entity in await GetByConditionAsync(expression))
            {
                await DeleteAsync(entity);
            }
        }
        #endregion

        #region query
        public Task<IQueryable<T>> GetAllAsync()
        {
            return Task.FromResult(Table.AsQueryable());
        }

        public Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(Table.Where(expression).AsQueryable());
        }
        public async Task<T> GetByIdAsync(TId id)
        {
            return await Table.FindAsync(id);
        }
        #endregion

        #region exist
        public async Task<bool> IsExistAsync(TId id)
        {
            return await Table.FindAsync(id) != null;
        }
        #endregion

        #region update
        public async Task<T> UpdateAsync(T entity)
        {
            AttachIfNot(entity);
            var entry = Table.Update(entity);
            await SaveAsync();
            return entry.Entity;
        }
        #endregion

        #region count
        public async Task<int> CountAsync()
        {
            return (await GetAllAsync()).Count();
        }

        public async Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return (await GetByConditionAsync(expression)).Count();
        }
        #endregion
    }
}