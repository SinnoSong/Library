using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Repository.Interface
{
    public interface IRepositoryBase<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        Task<bool> SaveAsync();

        Task<T> GetByIdAsync(TId id);

        Task<bool> IsExistAsync(TId id);
    }
}