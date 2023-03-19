using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Services
{
    /// <summary>
    /// Represents a service for performing CRUD operations on models of type T.
    /// </summary>
    /// <typeparam name="T">The type of model to perform CRUD operations on.</typeparam>
    public interface IService<T> : IDisposable
        where T : IModel
    {
        /// <summary>
        /// Creates a new model.
        /// </summary>
        /// <param name="model">The model to create.</param>
        /// <returns>The created model.</returns>
        Task<T> CreateAsync(T model);

        /// <summary>
        /// Modifies an existing model.
        /// </summary>
        /// <param name="model">The model to modify.</param>
        /// <returns>The modified model.</returns>
        Task<T> ModifyAsync(T model);

        /// <summary>
        /// Destroys a model with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the model to destroy.</param>
        /// <remarks>
        /// This method performs a hard delete, which removes the model from the database.
        /// </remarks>
        Task DestroyAsync(int id);

        /// <summary>
        /// Deletes a model with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the model to delete.</param>
        /// <remarks>
        /// This method performs a soft delete, which sets the model's "IsDeleted" property to true.
        /// </remarks>
        Task DeleteAsync(int id);

        /// <summary>
        /// Gets a model with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the model to get.</param>
        /// <returns>The model with the specified ID.</returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Gets a model with the specified ID using the provided query.
        /// </summary>
        /// <param name="id">The ID of the model to get.</param>
        /// <param name="func">A query that filters or orders the results.</param>
        /// <returns>The model with the specified ID.</returns>
        Task<T> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Gets all models of type T.
        /// </summary>
        /// <returns>An IEnumerable of all models of type T.</returns>
        Task<IEnumerable<T>> GetAsync();

        /// <summary>
        /// Gets all models of type T using the specified SQL query.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <returns>An IEnumerable of all models of type T that match the specified SQL query.</returns>
        Task<IEnumerable<T>> GetAsync(string sql);

        /// <summary>
        /// Gets all models of type T using the provided query.
        /// </summary>
        /// <param name="query">A query that filters or orders the results.</param>
        /// <returns>An IEnumerable of all models of type T that match the provided query.</returns>
        Task<IEnumerable<T>> GetAsync(IQueryable<T> query);

        /// <summary>
        /// Gets all models of type T using the provided query.
        /// </summary>
        /// <param name="func">A query that filters or orders the results.</param>
        /// <returns>An IEnumerable of all models of type T that match the provided query.</returns>
        Task<IEnumerable<T>> GetAsync(Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Retrieves all elements from the data source that match the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of elements that match the predicate.</returns>
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets all elements from the data source.
        /// </summary>
        /// <returns>The collection of all elements from the data source.</returns>
        IQueryable<T> Get();

        /// <summary>
        /// Gets elements from the data source using the specified SQL statement.
        /// </summary>
        /// <param name="sql">The SQL statement used to get the elements.</param>
        /// <returns>The collection of elements obtained using the specified SQL statement.</returns>
        IQueryable<T> Get(string sql);

        /// <summary>
        /// Gets elements from the data source using the specified function to modify the queryable before executing it.
        /// </summary>
        /// <param name="func">A function to modify the queryable before executing it.</param>
        /// <returns>The collection of elements obtained by executing the queryable after the modifications.</returns>
        IQueryable<T> Get(Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Gets elements from the data source that match the specified predicate.
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>The collection of elements that match the specified predicate.</returns>
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Retrieves the element with the specified id from the data source asynchronously.
        /// </summary>
        /// <param name="id">The id of the element to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the element with the specified id.</returns>
         Task<T> InspectAsync(int id);

        /// <summary>
        /// Retrieves the element with the specified id from the data source asynchronously using the specified function to modify the queryable before executing it.
        /// </summary>
        /// <param name="id">The id of the element to retrieve.</param>
        /// <param name="func">A function to modify the queryable before executing it.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the element with the specified id after executing the modifications specified by the function.</returns>
        Task<T> InspectAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);
    }
}
