using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Programatica.Framework.Core;

namespace Programatica.Framework.Data.Context
{
    public interface IDbContext : IObject
    {
        DbSet<TModel> Set<TModel>() where TModel : class;

        EntityEntry<TModel> Entry<TModel>(TModel entity) where TModel : class;

        int SaveChanges();

        void Dispose();
    }
}
