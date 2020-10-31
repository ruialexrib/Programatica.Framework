using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Context;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    public class RepositoryAsync<T> : IRepositoryAsync<T>, IDisposable
        where T : class, IModel
    {
        private readonly IDbContext _context;
        private bool _disposed = false;

        public RepositoryAsync(IDbContext context)
        {
            _context = context;
        }

        public virtual async Task<T> GetDataAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public IQueryable<T> GetData()
        {
                return _context.Set<T>().AsQueryable();
        }


        public async Task<T> InsertAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
