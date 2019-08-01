using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;

namespace Programatica.Framework.Data.Context
{
    public abstract class BaseDbContext : DbContext, IDbContext
    {
        public Guid InstanceSystemId { get; set; }
        public DateTime InstanceDateTime { get; set; }

        public DbSet<Audit> Audit { get; set; }
        public DbSet<TrackChange> TrackChanges { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
            InstanceSystemId = Guid.NewGuid();
            InstanceDateTime = DateTime.UtcNow;
        }

        DbSet<TModel> IDbContext.Set<TModel>()
        {
            return base.Set<TModel>();
        }

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
