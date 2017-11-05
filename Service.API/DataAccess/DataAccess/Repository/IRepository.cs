using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fantasy.API.DataAccess.Repository
{
    /// <summary>
    /// IRepository interface
    /// </summary>
    public interface IRepository<TEntity> : IDisposable
    {
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] predicatesToInclude);
        Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] predicatesToInclude);
        Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] predicatesToInclude);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<DateTime> GetSystemTimeAsync();
        Task<DateTime> GetSystemTimeStampAsync();
        Task<IEnumerable<T>> ExecuteQuery<T>(string cmd);
        Task<T> ExecuteQuerySingleAsync<T>(string cmd);
        Task ExecuteAsync(string cmd);
        Task<IEnumerable<TEntity>> PagedResultTaskAsync<TResult, TResult1>(Expression<Func<TEntity, bool>> predicate, int pageNum, int pageSize, Expression<Func<TEntity, TResult>> orderByFirstProperty, bool isFirstAscending,
            Expression<Func<TEntity, TResult1>> orderBySecondProperty, bool isSecondAscending);
        Task<IEnumerable<TEntity>> PagedResultTaskAsync<TResult>(Expression<Func<TEntity, bool>> predicate,
            int pageNum, int pageSize, Expression<Func<TEntity, TResult>> orderByProperty, bool isAscending);

        Task ExecuteAsync(string cmd, params object[] parameters);

    }
}
