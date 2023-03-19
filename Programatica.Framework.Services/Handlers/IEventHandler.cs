using Programatica.Framework.Data.Models;
using System.Threading.Tasks;

namespace Programatica.Framework.Services.Handlers
{
    /// <summary>
    /// Interface representing an event handler for CRUD operations on a model that implements <see cref="IModel"/>.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    public interface IEventHandler<T>
        where T : IModel
    {
        /// <summary>
        /// Event raised before creating a new instance of the model.
        /// </summary>
        /// <param name="model">The model being created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnBeforeCreatingAsync(T model);

        /// <summary>
        /// Event raised after creating a new instance of the model.
        /// </summary>
        /// <param name="model">The model that was created.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnAfterCreatedAsync(T model);

        /// <summary>
        /// Event raised before modifying an instance of the model.
        /// </summary>
        /// <param name="model">The model being modified.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnBeforeModifyingAsync(T model);

        /// <summary>
        /// Event raised after modifying an instance of the model.
        /// </summary>
        /// <param name="model">The model that was modified.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnAfterModifiedAsync(T model);

        /// <summary>
        /// Event raised before destroying an instance of the model.
        /// </summary>
        /// <param name="model">The model being destroyed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnBeforeDestroyingAsync(T model);

        /// <summary>
        /// Event raised after destroying an instance of the model.
        /// </summary>
        /// <param name="model">The model that was destroyed.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnAfterDestroyedAsync(T model);

        /// <summary>
        /// Event raised before deleting an instance of the model.
        /// </summary>
        /// <param name="model">The model being deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnBeforeDeletingAsync(T model);

        /// <summary>
        /// Event raised after deleting an instance of the model.
        /// </summary>
        /// <param name="model">The model that was deleted.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnAfterDeletedAsync(T model);

        /// <summary>
        /// Event raised before inspecting an instance of the model.
        /// </summary>
        /// <param name="model">The model being inspected.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task OnBeforeInspectingAsync(T model);
    }
}
