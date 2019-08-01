using System;

namespace Programatica.Framework.Core
{
    public interface IObject
    {
        Guid InstanceSystemId { get; set; }
        DateTime InstanceDateTime { get; set; }
    }
}
