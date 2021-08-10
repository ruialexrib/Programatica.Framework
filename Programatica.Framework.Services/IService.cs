using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    public interface IService<T> : IDisposable
        where T : IModel
    {
        Task<T> CreateAsync(T model);

        Task<T> ModifyAsync(T model);

        Task DestroyAsync(int id);

        Task DeleteAsync(int id);

        Task<T> GetAsync(int id);

        Task<T> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);

        Task<IEnumerable<T>> GetAsync();

        Task<IEnumerable<T>> GetAsync(string sql);

        Task<IEnumerable<T>> GetAsync(IQueryable<T> query);

        Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> func);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        IQueryable<T> Get();

        IQueryable<T> Get(string sql);

        IQueryable<T> Get(Func<IQueryable<T>, IQueryable<T>> func);

        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        Task<T> InspectAsync(int id);

        Task<T> InspectAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);
    }
}
