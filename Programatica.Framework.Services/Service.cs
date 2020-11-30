﻿using Programatica.Framework.Data.Models;
using Programatica.Framework.Services.Handlers;
using Programatica.Framework.Services.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
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
            try
            {
                model.CreatedDate = _injector.DateTimeAdapter.UtcNow;
                model.CreatedUser = _injector.AuthUserAdapter.Name;

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnBeforeCreatingAsync(model);
                }

                await _injector.TRepository.InsertAsync(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnAfterCreatedAsync(model);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> ModifyAsync(T model)
        {
            try
            {
                model.LastModifiedDate = _injector.DateTimeAdapter.UtcNow;
                model.LastModifiedUser = _injector.AuthUserAdapter.Name;

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnBeforeModifyingAsync(model);
                }

                await _injector.TRepository.UpdateAsync(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnAfterModifiedAsync(model);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                T record = await _injector.TRepository.GetDataAsync(id);

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnBeforeDeletingAsync(record);
                }

                await _injector.TRepository.DeleteAsync(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnAfterDeletedAsync(record);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DestroyAsync(int id)
        {
            try
            {
                T record = await _injector.TRepository.GetDataAsync(id);

                record.LastDestroyedDate = _injector.DateTimeAdapter.UtcNow;
                record.LastDestroyedUser = _injector.AuthUserAdapter.Name;
                record.IsDestroyed = true;

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnBeforeDestroyingAsync(record);
                }

                await _injector.TRepository.UpdateAsync(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnAfterDestroyedAsync(record);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> GetAsync(int id)
        {
            try
            {
                var result = await _injector.TRepository.GetDataAsync(id);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            try
            {
                return await _injector.TRepository.GetDataAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> InspectAsync(int id)
        {
            try
            {
                var result = await _injector.TRepository.GetDataAsync(id);

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    await handler.OnBeforeInspectingAsync(result);
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
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
