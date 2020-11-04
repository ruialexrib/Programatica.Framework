﻿using Programatica.Framework.Data.Models;
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
        T GetData(int id);
        int Insert(T entity);
        int InsertIfNew(T entity, Expression<Func<T, bool>> predicate);
        int Update(T entity);
        int Delete(T entity);
    }
}
