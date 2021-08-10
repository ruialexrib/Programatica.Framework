using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    public interface IRepository<T> : IDisposable
        where T : IModel
    {
        IQueryable<T> GetData();
        IQueryable<T> GetData(Func<IQueryable<T>, IQueryable<T>> func);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetDataAsync();
        Task<IEnumerable<T>> GetDataAsync(Func<IQueryable<T>, IQueryable<T>> func);
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetDataAsync(int id);
        Task<T> GetDataAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);

        Task<int> InsertAsync(T entity);
        Task<int> InsertIfNewAsync(T entity, Expression<Func<T, bool>> predicate);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
