using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Programatica.Framework.Data.Extensions
{
    /// <summary>
    /// Contains extension methods for the DatabaseFacade class.
    /// </summary>
    public static class DatabaseFacadeExtensions
    {
        /// <summary>
        /// Determines whether the database exists.
        /// </summary>
        /// <param name="source">The DatabaseFacade instance.</param>
        /// <returns>True if the database exists, false otherwise.</returns>
        public static bool Exists(this DatabaseFacade source)
        {
            return source.GetService<IRelationalDatabaseCreator>().Exists();
        }
    }
}
