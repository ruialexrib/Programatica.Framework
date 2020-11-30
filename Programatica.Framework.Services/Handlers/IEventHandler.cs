using Programatica.Framework.Data.Models;
using System.Threading.Tasks;

namespace Programatica.Framework.Services.Handlers
{
    public interface IEventHandler<T>
        where T : IModel
    {
        Task OnBeforeCreatingAsync(T model);
        Task OnAfterCreatedAsync(T model);
        Task OnBeforeModifyingAsync(T model);
        Task OnAfterModifiedAsync(T model);
        Task OnBeforeDestroyingAsync(T model);
        Task OnAfterDestroyedAsync(T model);
        Task OnBeforeDeletingAsync(T model);
        Task OnAfterDeletedAsync(T model);
        Task OnBeforeInspectingAsync(T model);
    }
}
