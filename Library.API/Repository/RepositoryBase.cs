using Library.API.Entities;
using Library.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Repository
{
    public class RepositoryBase<T, TId> : IRepositoryBase<T, TId> where T : class
    {
        private readonly LibraryDbContext _dbContext;

        public virtual DbSet<T> Table => _dbContext.Set<T>();

        public RepositoryBase(LibraryDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void Create(T entity)
        {
            Table.Add(entity);
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(Table.AsEnumerable());
        }

        public Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(Table.Where(expression).AsEnumerable());
        }

        public async Task<T> GetByIdAsync(TId id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<bool> IsExistAsync(TId id)
        {
            return await Table.FindAsync(id) != null;
        }

        public async Task<bool> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }
    }
}