using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    public interface IService<T> : IDisposable
        where T : IModel
    {
        Task<T> CreateAsync(T model);
        T Create(T model);

        Task<T> ModifyAsync(T model);
        T Modify(T model);

        Task DestroyAsync(int id);
        void Destroy(int id);

        Task DeleteAsync(int id);
        void Delete(int id);

        Task<T> GetAsync(int id);
        T Get(int id);

        Task<IEnumerable<T>> GetAsync();
        IQueryable<T> Get();

        Task<T> InspectAsync(int id);
        T Inspect(int id);
    }
}
