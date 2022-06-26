using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace Programatica.Framework.Data.Context
{
    public interface IDbContext /*: IObject*/
    {
        DatabaseFacade Database { get; }

        DbSet<TModel> Set<TModel>() where TModel : class;

        EntityEntry<TModel> Entry<TModel>(TModel entity) where TModel : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();
        void Migrate();
        void Dispose();
    }
}
