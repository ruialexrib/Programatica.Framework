using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Programatica.Framework.Data.Context;
using Programatica.Framework.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Repository
{
    /// <summary>
    /// This is a generic repository class that provides common CRUD operations for a given entity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Repository<T> : IRepository<T>, IDisposable
        where T : class, IModel
    {
        protected readonly IDbContext _context;
        private bool _disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}"/> class with the specified database context.
        /// </summary>
        /// <param name="context">The database context to be used.</param>
        public Repository(IDbContext context) : base()
        {
            _context = context;
        }

        /// <summary>
        /// Gets the <see cref="DbSet{TEntity}"/> for the given entity type.
        /// </summary>
        protected DbSet<T> DbSet
        {
            get
            {
                return _context.Set<T>();
            }
        }

        #region IRepository<T>

        /// <summary>
        /// Retrieves all entities from the database without tracking changes.
        /// </summary>
        /// <returns>An <see cref="IQueryable{T}"/> representing the entities.</returns>
        /// <example>
        /// var entities = repository.GetData().Where(x => x.IsActive);
        /// </example>
        public IQueryable<T> GetData()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// Retrieves data from the database using a raw SQL query.
        /// </summary>
        /// <param name="sql">The raw SQL query to execute.</param>
        /// <returns>An <see cref="IQueryable{T}"/> object containing the query results.</returns>
        /// <example>
        /// var orders = repository.GetData("SELECT * FROM Orders WHERE Status = 'Pending'");
        /// </example>
        public IQueryable<T> GetData(string sql)
        {
            return DbSet.FromSql<T>(sql);
        }

        /// <summary>
        /// Returns a custom queryable set of data by accepting a function that takes in an IQueryable<T> and returns another IQueryable<T>.
        /// </summary>
        /// <param name="func">A function that takes in an IQueryable<T> and returns another IQueryable<T> for custom querying of data.
        /// <returns>An IQueryable<T> object representing the queried data.</returns>
        /// <example>
        /// repository.GetData(q => q.Where(x => x.Name.Contains("John")).OrderByDescending(x => x.Id))</param>
        /// </example>
        public IQueryable<T> GetData(Func<IQueryable<T>, IQueryable<T>> func)
        {
            var query = DbSet as IQueryable<T>;
            IQueryable<T> queryWithEagerLoading = func(query);
            return queryWithEagerLoading.AsNoTracking();
        }

        /// <summary>
        /// Returns a queryable set of data that satisfies the specified condition.
        /// </summary>
        /// <param name="predicate">The expression representing the condition to be satisfied.</param>
        /// <returns>An IQueryable<T> object representing the queried data.</returns>
        /// <example>
        /// repository.GetWhere(x => x.Name == "John")
        /// </example>
        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        /// <summary>
        /// Retrieves all entities of type T asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation that returns an enumerable collection of entities of type T.</returns>
        /// <example>
        /// var repository = new Repository<Person>(dbContext);
        /// var people = await repository.GetDataAsync();  
        /// </example>
        public async Task<IEnumerable<T>> GetDataAsync()
        {
            return await DbSet.AsNoTracking()
                              .ToListAsync();
        }

        /// <summary>
        /// Gets data asynchronously from the database using a raw SQL query.
        /// </summary>
        /// <param name="sql">The SQL query to execute.</param>
        /// <returns>An enumerable collection of <typeparamref name="T"/> objects retrieved from the database.</returns>
        /// <remarks>The results of the query are tracked by the context.</remarks>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="sql"/> is null.</exception>
        public async Task<IEnumerable<T>> GetDataAsync(string sql)
        {
            return await DbSet.FromSql<T>(sql).ToListAsync();
        }

        /// <summary>
        /// Gets data from the database asynchronously, applying any modifications specified by the input function.
        /// </summary>
        /// <param name="func">A function that modifies the <see cref="IQueryable{T}"/> object before data is retrieved.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> object containing the retrieved data.</returns>
        /// <example>
        /// var products = await GetDataAsync(q => q.Where(p => p.Price > 10.00));
        /// </example>
        public async Task<IEnumerable<T>> GetDataAsync(Func<IQueryable<T>, IQueryable<T>> func)
        {
            var query = DbSet as IQueryable<T>;
            IQueryable<T> queryWithEagerLoading = func(query);
            return await queryWithEagerLoading.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// Retrieves all entities that match the specified predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>A collection of entities that match the specified predicate.</returns>
        /// <example>
        /// var orders = await orderRepository.GetWhereAsync(o => o.OrderDate >= new DateTime(2022, 1, 1));
        /// </example>
        public async Task<IEnumerable<T>> GetWhereAsync(Expression<Func<T, bool>> predicate)
        {
            return await DbSet.Where(predicate)
                              .ToListAsync();
        }

        /// <summary>
        /// Retrieves a single entity by its ID asynchronously and returns it.
        /// </summary>
        /// <param name="id">The ID of the entity to retrieve.</param>
        /// <returns>The retrieved entity, or null if the entity was not found.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while retrieving the entity.</exception>
        /// <example>
        /// var customer = await GetDataAsync(1);
        /// </example>
        public async Task<T> GetDataAsync(int id)
        {
            try
            {
                var entity = await DbSet.Where(x => x.Id == id)
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync();

                _context.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves a single entity of type T by its Id with eager loading using the provided function
        /// </summary>
        /// <param name="id">The Id of the entity to retrieve</param>
        /// <param name="func">A function to include related entities with eager loading</param>
        /// <returns>The retrieved entity</returns>
        /// <example>
        /// var productId = 1;
        /// var product = await _productRepository.GetDataAsync(productId, query => 
        ///     query.Include(p => p.Category)
        ///          .Include(p => p.Supplier));
        /// </example>
        public async Task<T> GetDataAsync(int id, Func<IQueryable<T>, IQueryable<T>> func)
        {

            var entity = DbSet.Where(x => x.Id == id);
            IQueryable<T> entityWithEagerLoading = func(entity);
            return await entityWithEagerLoading.AsNoTracking().FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts a new entity into the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <returns>The number of rows affected.</returns>
        /// <example>
        ///     var product = new Product { Name = "Product A", Price = 9.99 };
        ///     var numSaved = await _repository.InsertAsync(product);
        /// </example>
        public async Task<int> InsertAsync(T entity)
        {
            try
            {
                DbSet.Add(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Inserts the specified entity into the database only if it doesn't exist yet.
        /// </summary>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="predicate">The predicate to check if an entity already exists in the database.</param>
        /// <returns>The number of affected rows: 1 if the entity was inserted, 0 if it already existed in the database.</returns>
        /// <example>
        /// var customer = new Customer { Name = "John Smith", Age = 30 };
        /// var result = await customerRepository.InsertIfNewAsync(customer, c => c.Name == customer.Name);
        /// </example>
        public async Task<int> InsertIfNewAsync(T entity, Expression<Func<T, bool>> predicate)
        {
            try
            {
                var record = await DbSet.AsQueryable()
                                  .Where(predicate)
                                  .SingleOrDefaultAsync();

                if (record == null)
                {
                    return await InsertAsync(entity);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates the specified entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The number of rows affected.</returns>
        /// <exception cref="ArgumentNullException">Thrown when the entity is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown when the entity is not being tracked by the context.</exception>
        /// <example>
        /// var entityToUpdate = await myRepository.GetDataAsync(1);
        /// entityToUpdate.Name = "New Name";
        /// await myRepository.UpdateAsync(entityToUpdate);
        /// </example>
        public async Task<int> UpdateAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                _context.Entry(entity).State = EntityState.Modified;
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Deletes the specified entity from the database.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>The number of entities deleted from the database.</returns>
        /// <exception cref="Exception">Thrown if an error occurs while deleting the entity.</exception>
        /// <example>
        /// var entityToDelete = new MyEntity { Id = 1 };
        /// var result = await _myRepository.DeleteAsync(entityToDelete);
        /// </example>
        public async Task<int> DeleteAsync(T entity)
        {
            try
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
                return await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion IRepository<T>

        #region IDisposable

        /// <summary>
        /// Disposes the repository's context if it has not already been disposed.
        /// </summary>
        /// <param name="disposing">Whether or not to dispose the context.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
