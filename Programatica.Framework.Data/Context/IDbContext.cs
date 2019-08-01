using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Programatica.Framework.Core;

namespace Programatica.Framework.Data.Context
{
    public interface IDbContext : IObject
    {
        DatabaseFacade Database { get; }

        DbSet<TModel> Set<TModel>() where TModel : class;

        EntityEntry<TModel> Entry<TModel>(TModel entity) where TModel : class;

        int SaveChanges();

        void Dispose();
    }
}
