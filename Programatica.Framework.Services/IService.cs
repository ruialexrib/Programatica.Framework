using Programatica.Framework.Core;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;

namespace Programatica.Framework.Services
{
    public interface IService<T> : IDisposable
        where T : IModel
    {
        T Create(T model);
        T Modify(T model);
        void Destroy(int id);
        void Delete(int id);
        T Get(int id);
        IQueryable<T> Get();
        T Inspect(int id);
    }
}
