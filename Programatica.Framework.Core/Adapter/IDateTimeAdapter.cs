using System;

namespace Programatica.Framework.Core.Adapter
{
    public interface IDateTimeAdapter 
    {
        DateTime UtcNow { get; }
    }
}
