using Programatica.Framework.Core;
using Programatica.Framework.Data.Models;

namespace Programatica.Framework.Services.Handlers
{
    public interface IEventHandler<T> 
        where T : IModel
    {
        void OnBeforeCreating(T model);
        void OnAfterCreated(T model);
        void OnBeforeModifying(T model);
        void OnAfterModified(T model);
        void OnBeforeDestroying(T model);
        void OnAfterDestroyed(T model);
        void OnBeforeDeleting(T model);
        void OnAfterDeleted(T model);
        void OnBeforeInspecting(T model);
    }
}
