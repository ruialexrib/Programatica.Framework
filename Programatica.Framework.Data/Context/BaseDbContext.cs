using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Core;
using Programatica.Framework.Data.Models;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Programatica.Framework.Data.Context
{
    public abstract class BaseDbContext : DbContext, IDbContext, IObject
    {
        public Guid InstanceSystemId { get; set; }
        public DateTime InstanceDateTime { get; set; }

        public string GetCallerMemberName([CallerMemberName] string callingMember = null)
        {
            return callingMember;
        }

        public string GetCallerFilePath([CallerFilePath] string callingFile = null)
        {
            return callingFile;
        }

        public int GetCallerLineNumber([CallerLineNumber] int callingLineNum = 0)
        {
            return callingLineNum;
        }

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
