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
        Task<IEnumerable<T>> GetDataAsync();
        Task<T> GetDataAsync(int id);
        T GetData(int id);
        Task<int> InsertAsync(T entity);
        int Insert(T entity);
        Task<int> InsertIfNewAsync(T entity, Expression<Func<T, bool>> predicate);
        int InsertIfNew(T entity, Expression<Func<T, bool>> predicate);
        Task<int> UpdateAsync(T entity);
        int Update(T entity);
        Task<int> DeleteAsync(T entity);
        int Delete(T entity);
    }
}
