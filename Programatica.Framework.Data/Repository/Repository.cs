using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Context;
using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable
        where T : class, IModel
    {
        protected readonly IDbContext _context;
        private bool _disposed = false;

        public Repository(IDbContext context) : base()
        {
            _context = context;
        }

        protected DbSet<T> DbSet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        #region IRepository<T>

        public async Task<IEnumerable<T>> GetDataAsync()
        {
            return await DbSet.AsNoTracking()
                              .ToListAsync();
        }

        public IQueryable<T> GetData()
        {
            try
            {
                return DbSet.AsNoTracking()
                            .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetDataAsync(int id)
        {
            try
            {
                var entity = await DbSet.Where(x => x.Id == id)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();

                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetData(int id)
        {
            try
            {
                var entity = DbSet.Find(id);
                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertAsync(T entity)
        {
            try
            {
                DbSet.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Insert(T entity)
        {
            try
            {
                DbSet.Add(entity);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> InsertIfNewAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var record = await DbSet.AsQueryable()
                                  .Where(predicate)
                                  .SingleOrDefaultAsync();

                if (record == null)
                {
                    return await InsertAsync(entity);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertIfNew(T entity, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var record = DbSet.AsQueryable()
                                  .Where(predicate)
                                  .SingleOrDefault();

                if (record == null)
                {
                    return Insert(entity);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Delete(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion IRepository<T>

        #region IDisposable

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

        #endregion IDisposable
    }
}
