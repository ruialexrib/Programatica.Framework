using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    public interface IServiceAsync<T> : IDisposable
        where T : IModel
    {
        Task<T> CreateAsync(T model);
        Task<T> ModifyAsync(T model);
        Task DestroyAsync(int id);
        Task DeleteAsync(int id);
        Task<T> GetAsync(int id);
        Task<IReadOnlyList<T>> GetAsync();
        Task<T> InspectAsync(int id);
    }
}
