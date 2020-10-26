using System.Collections.Generic;

namespace Programatica.Framework.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<T> OrEmptyIfNull<T>(this List<T> source)
        {
            return source ?? new List<T>();
        }
    }
}
