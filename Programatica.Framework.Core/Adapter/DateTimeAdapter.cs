using System;

namespace Programatica.Framework.Core.Adapter
{
    public class DateTimeAdapter : IDateTimeAdapter
    {
        public DateTimeAdapter() : base()
        { }
        public DateTime UtcNow
        {
            get { return DateTime.UtcNow; }
        }
    }
}
