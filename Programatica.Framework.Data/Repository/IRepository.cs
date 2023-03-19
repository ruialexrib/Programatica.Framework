using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    /// <summary>
    /// Generic repository interface for data access operations on <typeparamref name="T"/> models.
    /// </summary>
    /// <typeparam name="T">Type of the model to be handled by the repository. Must implement <see cref="IModel"/>.</typeparam>
    public interface IRepository<T> : IDisposable
        where T : IModel
    {
        /// <summary>
        /// Gets all instances of <typeparamref name="T"/> from the underlying data store.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> instance representing the result set.</returns>
        IQueryable<T> GetData();

        /// <summary>
        /// Gets all instances of <typeparamref name="T"/> from the underlying data store using a custom SQL statement.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>An <see cref="IQueryable{T}"/> instance representing the result set.</returns>
        IQueryable<T> GetData(string sql);

        /// <summary>
        /// Gets all instances of <typeparamref name="T"/> from the underlying data store, applying a query transformation function.
        /// </summary>
        /// <param name="func">A function that takes an <see cref="IQueryable{T}"/> as input and returns a transformed <see cref="IQueryable{T}"/>.</param>
        /// <returns>An <see cref="IQueryable{T}"/> instance representing the result set.</returns>
        IQueryable<T> GetData(Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Gets all instances of <typeparamref name="T"/> from the underlying data store that satisfy a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the data store.</param>
        /// <returns>An <see cref="IQueryable{T}"/> instance representing the result set.</returns>
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously gets all instances of <typeparamref name="T"/> from the underlying data store.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> instance representing the result set.</returns>
        Task<IEnumerable<T>> GetDataAsync();

        /// <summary>
        /// Asynchronously gets all instances of <typeparamref name="T"/> from the underlying data store using a custom SQL statement.
        /// </summary>
        /// <param name="sql">The SQL statement to execute.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> instance representing the result set.</returns>
        Task<IEnumerable<T>> GetDataAsync(string sql);

        /// <summary>
        /// Asynchronously gets all instances of <typeparamref name="T"/> from the underlying data store, applying a query transformation function.
        /// </summary>
        /// <param name="func">A function that takes an <see cref="IQueryable{T}"/> as input and returns a transformed <see cref="IQueryable{T}"/>.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> instance representing the result set.</returns>
        Task<IEnumerable<T>> GetDataAsync(Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Asynchronously gets all instances of <typeparamref name="T"/> from the underlying data store that satisfy a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to apply to the data store.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> instance representing the result set.</returns>
        Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Asynchronously gets the entity with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity with the specified ID.</returns>
        Task<T> GetDataAsync(int id);

        /// <summary>
        /// Asynchronously gets the entity with the specified ID using the specified function to modify the query before execution.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <param name="func">A function to modify the query before execution.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the entity with the specified ID.</returns>
        Task<T> GetDataAsync(int id, Func<IQueryable<T>, IQueryable<T>> func);

        /// <summary>
        /// Asynchronously inserts a new entity into the repository.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of entities written to the database.</returns>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Asynchronously inserts a new entity into the repository if it does not already exist based on the specified predicate.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="predicate">A predicate used to determine if the entity already exists.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the number of entities written to the database.</returns>
        Task<int> InsertIfNewAsync(T entity, Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Updates the specified entity asynchronously in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Deletes the specified entity asynchronously from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the number of state entries written to the database.</returns>
        Task<int> DeleteAsync(T entity);

    }
}
