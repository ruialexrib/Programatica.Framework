using Programatica.Framework.Core;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Programatica.Framework.Data.Repository
{
    public interface IRepository<T> : IObject, IDisposable
        where T : IModel
    {
        IQueryable<T> GetData();
        T GetData(int id);
        int Insert(T entity);
        int InsertIfNew(T entity, Expression<Func<T, bool>> predicate);
        int Update(T entity);
        int Delete(T entity);
    }
}
