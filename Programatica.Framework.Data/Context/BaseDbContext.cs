using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Core.Exceptions;
using Programatica.Framework.Data.Extensions;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;

namespace Programatica.Framework.Data.Context
{
    /// <summary>
    /// Provides a base implementation for a database context.
    /// </summary>
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        /// <summary>
        /// Gets or sets a DbSet for the audit logs.
        /// </summary>
        public DbSet<Audit> Audit { get; set; }

        /// <summary>
        /// Gets or sets a DbSet for the track changes.
        /// </summary>
        public DbSet<TrackChange> TrackChanges { get; set; }

        /// <summary>
        /// Initializes a new instance of the BaseDbContext class with the specified options.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Gets a DbSet for the specified entity type.
        /// </summary>
        /// <typeparam name="TModel">The type of entity to retrieve.</typeparam>
        /// <returns>A DbSet for the specified entity type.</returns>
        DbSet<TModel> IDbContext.Set<TModel>()
        {
            return base.Set<TModel>();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            var changeSet = ChangeTracker.Entries();

            if (changeSet != null)
            {
                foreach (var entry in changeSet.Where(c => c.State == EntityState.Added))
                {
                    if (typeof(IModel).IsAssignableFrom(entry.Entity.GetType()))
                    {
                        var model = (IModel)entry.Entity;
                    }
                }
            }
            return base.SaveChanges();
        }

        /// <summary>
        /// Applies any pending migrations for the context to the database.
        /// </summary>
        public void Migrate()
        {
            var db = base.Database;
            try
            {
                if (!db.Exists())
                {
                    base.Database.Migrate();
                }
                else
                {
                    throw new DbContextException("Database already exists.");
                }
            }
            catch (Exception e)
            {
                throw new DbContextException(e.Message, e);
            }
        }
    }
}
