using Programatica.Framework.Core.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programatica.Framework.Core
{
    public abstract class BaseObject : IObject
    {
        [NotMapped]
        [NotTrackable]
        public Guid InstanceSystemId { get; set; }

        [NotMapped]
        [NotTrackable]
        public DateTime InstanceDateTime { get; set; }

        public BaseObject()
        {
            InstanceSystemId = Guid.NewGuid();
            InstanceDateTime = DateTime.UtcNow;
        }
    }
}
