using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Context
{
    /// <summary>
    /// Represents a database context used for accessing and managing entities in a database.
    /// </summary>
    public interface IDbContext /*: IObject*/
    {
        /// <summary>
        /// Gets the database instance used by the context.
        /// </summary>
        DatabaseFacade Database { get; }

        /// <summary>
        /// Gets a DbSet for the specified entity type.
        /// </summary>
        /// <typeparam name="TModel">The type of entity to retrieve.</typeparam>
        /// <returns>A DbSet for the specified entity type.</returns>
        DbSet<TModel> Set<TModel>() where TModel : class;

        /// <summary>
        /// Gets the EntityEntry for the specified entity.
        /// </summary>
        /// <typeparam name="TModel">The type of the entity.</typeparam>
        /// <param name="entity">The entity for which to get the EntityEntry.</param>
        /// <returns>The EntityEntry for the specified entity.</returns>
        EntityEntry<TModel> Entry<TModel>(TModel entity) where TModel : class;

        /// <summary>
        /// Asynchronously saves all changes made in the context to the database.
        /// </summary>
        /// <param name="cancellationToken">A token to cancel the save operation.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Saves all changes made in the context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        int SaveChanges();

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// </summary>
        void Migrate();

        /// <summary>
        /// Disposes the context and frees any resources used by it.
        /// </summary>
        void Dispose();
    }

}
