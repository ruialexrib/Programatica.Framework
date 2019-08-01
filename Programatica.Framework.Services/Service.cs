using Programatica.Framework.Core;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Services.Handlers;
using Programatica.Framework.Services.Injector;
using System;
using System.Linq;

namespace Programatica.Framework.Services
{
    public class Service<T> : BaseObject, IService<T>
        where T : IModel
    {
        protected internal readonly IInjector<T> _injector;

        public Service(IInjector<T> injector) :base()
        {
            _injector = injector;
        }

        #region IService<T>

        public virtual T Create(T model)
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

                _injector.TRepository.Insert(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterCreated(model);
                }

                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual T Modify(T model)
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

                _injector.TRepository.Update(model);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterModified(model);
                }

                return model;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual void Delete(int id)
        {
            try
            {
                T record = _injector.TRepository.GetData(id);

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnBeforeDeleting(record);
                }

                _injector.TRepository.Delete(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterDeleted(record);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Destroy(int id)
        {
            try
            {
                T record = _injector.TRepository.GetData(id);

                record.LastDestroyedDate = _injector.DateTimeAdapter.UtcNow;
                record.LastDestroyedUser = _injector.AuthUserAdapter.Name;
                record.IsDestroyed = true;

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnBeforeDestroying(record);
                }

                _injector.TRepository.Update(record);

                // handle after events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnAfterDestroyed(record);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual T Get(int id)
        {
            try
            {
                var result = _injector.TRepository.GetData(id);
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual IQueryable<T> Get()
        {
            try
            {
                return _injector.TRepository.GetData();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual T Inspect(int id)
        {
            try
            {
                var result = _injector.TRepository.GetData(id);

                // handle before events  
                foreach (IEventHandler<T> handler in _injector.EventHandlers)
                {
                    handler.OnBeforeInspecting(result);
                }

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion IService<T>

        #region IDisposable

        public void Dispose()
        {
            _injector.TRepository.Dispose();
        }

        #endregion IDisposable

    }
}
