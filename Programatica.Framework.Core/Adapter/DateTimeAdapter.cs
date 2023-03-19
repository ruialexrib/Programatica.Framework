using System;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Represents an implementation of the <see cref="IDateTimeAdapter"/> interface that returns the current UTC date and time.
    /// </summary>
    public class DateTimeAdapter : IDateTimeAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimeAdapter"/> class.
        /// </summary>
        public DateTimeAdapter() : base() { }

        /// <summary>
        /// Gets the current UTC date and time.
        /// </summary>
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
