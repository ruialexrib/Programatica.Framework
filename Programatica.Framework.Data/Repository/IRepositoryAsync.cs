using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    public interface IRepositoryAsync<T> : IDisposable
        where T : IModel
    {
        Task<T> GetDataAsync(int id);
        Task<IReadOnlyList<T>> GetDataAsync();
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
