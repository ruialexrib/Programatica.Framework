using Programatica.Framework.Core;
using Programatica.Framework.Core.Attributes;
using System;

namespace Programatica.Framework.Data.Models
{
    public abstract class BaseModel : BaseObject, IModel
    {
        [NotTrackable]
        public Guid SystemId { get; set; }
        [NotTrackable]
        public int Id { get; set; }
        public string Comments { get; set; }
        [NotTrackable]
        public DateTime? CreatedDate { get; set; }
        [NotTrackable]
        public string CreatedUser { get; set; }
        [NotTrackable]
        public DateTime? LastModifiedDate { get; set; }
        [NotTrackable]
        public string LastModifiedUser { get; set; }
        [NotTrackable]
        public DateTime? LastDestroyedDate { get; set; }
        [NotTrackable]
        public string LastDestroyedUser { get; set; }
        [NotTrackable]
        public bool IsSystem { get; set; }
        public bool IsDestroyed { get; set; }

        public BaseModel() : base()
        {
            SystemId = Guid.NewGuid();
        }
    }
}
