using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Services.Interface
{
    public interface IBaseServices<T, TId>
    {
        #region query
        Task<IQueryable<T>> GetAllAsync();
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(TId id);
        #endregion

        #region add
        Task<T> AddAsync(T entity);
        Task AddAsync(IEnumerable<T> entities);
        #endregion

        #region update
        Task<T> UpdateAsync(T entity);
        #endregion

        #region delete
        Task<bool> DeleteAsync(T entity);
        Task DeleteByConditionAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region count
        Task<int> CountAsync();
        Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression);
        #endregion

        #region exist
        Task<bool> IsExistAsync(TId id);
        #endregion
    }
}
