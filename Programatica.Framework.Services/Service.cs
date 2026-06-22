using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Services.Handlers;
using Programatica.Framework.Services.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    public class Service<T> : IService<T>
        where T : IModel
    {
        protected internal readonly IInjector<T> _injector;

        public Service(IInjector<T> injector) : base()
        {
            _injector = injector;
        }

        #region IService<T>

        public async Task<T> CreateAsync(T model)
        {
            model.CreatedDate = _injector.DateTimeAdapter.UtcNow;
            model.CreatedUser = _injector.AuthUserAdapter.Name;

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeCreatingAsync(model);
            }

            await _injector.TRepository.InsertAsync(model);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnAfterCreatedAsync(model);
            }

            return model;
        }

        public async Task<T> ModifyAsync(T model)
        {
            model.LastModifiedDate = _injector.DateTimeAdapter.UtcNow;
            model.LastModifiedUser = _injector.AuthUserAdapter.Name;

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeModifyingAsync(model);
            }

            await _injector.TRepository.UpdateAsync(model);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnAfterModifiedAsync(model);
            }

            return model;
        }

        public async Task DeleteAsync(int id)
        {
            T record = await _injector.TRepository.GetDataAsync(id);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeDeletingAsync(record);
            }

            await _injector.TRepository.DeleteAsync(record);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnAfterDeletedAsync(record);
            }
        }

        public async Task DestroyAsync(int id)
        {
            T record = await _injector.TRepository.GetDataAsync(id);

            record.LastDestroyedDate = _injector.DateTimeAdapter.UtcNow;
            record.LastDestroyedUser = _injector.AuthUserAdapter.Name;
            record.IsDestroyed = true;

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeDestroyingAsync(record);
            }

            await _injector.TRepository.UpdateAsync(record);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnAfterDestroyedAsync(record);
            }
        }

        public async Task<IEnumerable<T>> GetAsync(IQueryable<T> query)
        {
            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _injector.TRepository.GetDataAsync(id);
        }

        public async Task<T> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>> func)
        {
            return await _injector.TRepository.GetDataAsync(id, func);
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _injector.TRepository.GetWhereAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await _injector.TRepository.GetDataAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(string sql)
        {
            return await _injector.TRepository.GetDataAsync(sql);
        }

        public async Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> func)
        {
            return await _injector.TRepository.GetDataAsync(func);
        }

        public IQueryable<T> Get()
        {
            return _injector.TRepository.GetData();
        }

        public IQueryable<T> Get(string sql)
        {
            return _injector.TRepository.GetData(sql);
        }

        public IQueryable<T> Get(Func<IQueryable<T>, IQueryable<T>> func)
        {
            return _injector.TRepository.GetData(func);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _injector.TRepository.GetWhere(predicate);
        }

        public async Task<T> InspectAsync(int id)
        {
            var result = await _injector.TRepository.GetDataAsync(id);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeInspectingAsync(result);
            }

            return result;
        }

        public async Task<T> InspectAsync(int id, Func<IQueryable<T>, IQueryable<T>> func)
        {
            var result = await _injector.TRepository.GetDataAsync(id, func);

            foreach (IEventHandler<T> handler in _injector.EventHandlers)
            {
                await handler.OnBeforeInspectingAsync(result);
            }

            return result;
        }

        #endregion IService<T>

        #region IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _injector.TRepository.Dispose();
        }

        #endregion IDisposable

    }
}
