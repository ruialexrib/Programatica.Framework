using System;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Represents an interface for a DateTimeAdapter, used to abstract the system's DateTime functionality.
    /// </summary>
    public interface IDateTimeAdapter 
    {
        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
