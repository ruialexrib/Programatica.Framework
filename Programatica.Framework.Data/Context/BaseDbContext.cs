using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Models;
using System.Linq;

namespace Programatica.Framework.Data.Context
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        public DbSet<Audit> Audit { get; set; }
        public DbSet<TrackChange> TrackChanges { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<TModel> IDbContext.Set<TModel>()
        {
            return base.Set<TModel>();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
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

    }
}
