using Programatica.Framework.Data.Models;
using System.Threading.Tasks;

namespace Programatica.Framework.Services.Handlers
{
    public interface IEventHandler<T>
        where T : IModel
    {
        Task OnBeforeCreatingAsync(T model);
        void OnBeforeCreating(T model);

        Task OnAfterCreatedAsync(T model);
        void OnAfterCreated(T model);

        Task OnBeforeModifyingAsync(T model);
        void OnBeforeModifying(T model);

        Task OnAfterModifiedAsync(T model);
        void OnAfterModified(T model);

        Task OnBeforeDestroyingAsync(T model);
        void OnBeforeDestroying(T model);

        Task OnAfterDestroyedAsync(T model);
        void OnAfterDestroyed(T model);

        Task OnBeforeDeletingAsync(T model);
        void OnBeforeDeleting(T model);

        Task OnAfterDeletedAsync(T model);
        void OnAfterDeleted(T model);

        Task OnBeforeInspectingAsync(T model);
        void OnBeforeInspecting(T model);
    }
}
