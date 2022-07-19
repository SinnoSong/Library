using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.API.Service.Interface;

public interface IBaseService<T, TId>
{
    #region update

    Task<T> UpdateAsync(T entity);

    #endregion

    #region exist

    Task<bool> IsExistAsync(TId id);

    #endregion

    #region save

    Task<bool> SaveAsync();

    #endregion

    #region query

    Task<IQueryable<T>> GetAllAsync();
    Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
    Task<T> GetByIdAsync(TId id);

    #endregion

    #region add

    Task<T> AddAsync(T entity);
    Task AddAsync(IEnumerable<T> entities);

    #endregion

    #region delete

    Task DeleteAsync(T entity);
    Task DeleteByConditionAsync(Expression<Func<T, bool>> expression);

    #endregion

    #region count

    Task<int> CountAsync();
    Task<int> CountByConditionAsync(Expression<Func<T, bool>> expression);

    #endregion
}