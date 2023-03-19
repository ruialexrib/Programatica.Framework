using System.Collections.Generic;

namespace Programatica.Framework.Core.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="List{T}"/> class.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the input list if it's not null, or an empty list if it is.
        /// </summary>
        /// <typeparam name="T">The type of elements in the list.</typeparam>
        /// <param name="source">The input list.</param>
        /// <returns>The input list if it's not null, or an empty list if it is.</returns>
        public static List<T> OrEmptyIfNull<T>(this List<T> source)
        {
            return source ?? new List<T>();
        }
    }
}
