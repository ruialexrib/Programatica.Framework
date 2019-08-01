using System;

namespace Programatica.Framework.Core.Adapter
{
    public interface IDateTimeAdapter : IObject
    {
        DateTime UtcNow { get; }
    }
}
