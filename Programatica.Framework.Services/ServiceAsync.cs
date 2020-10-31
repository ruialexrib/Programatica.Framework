using Programatica.Framework.Data.Models;
using Programatica.Framework.Services.Handlers;
using Programatica.Framework.Services.Injector;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    public class ServiceAsync<T> : IServiceAsync<T>
        where T : IModel
    {

        protected internal readonly IInjectorAsync<T> _injector;

        public ServiceAsync(IInjectorAsync<T> injector) : base()
        {
            _injector = injector;
        }


        public async Task<T> CreateAsync(T model)
        {
            try
            {
                model.CreatedDate = _injector.DateTimeAdapter.UtcNow;
                model.CreatedUser = _injector.AuthUserAdapter.Name;

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnBeforeCreating(model);
                }

                await _injector.TRepository.InsertAsync(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterCreated(model);
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
                    handler.OnBeforeDeleting(record);
                }

                await _injector.TRepository.DeleteAsync(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterDeleted(record);
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
                    handler.OnBeforeDestroying(record);
                }

                await _injector.TRepository.UpdateAsync(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterDestroyed(record);
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

        public IQueryable<T> Get()
        {
            try
            {
                return _injector.TRepository.GetData();
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
                    handler.OnBeforeInspecting(result);
                }

                return result;
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
                    handler.OnBeforeModifying(model);
                }

                await _injector.TRepository.UpdateAsync(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterModified(model);
                }

                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            _injector.TRepository.Dispose();
        }
    }
}
