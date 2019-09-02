using Programatica.Framework.Core.Attributes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

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

        public string GetCallerMemberName([CallerMemberName] string callingMember = null)
        {
            return callingMember;
        }

        public string GetCallerFilePath([CallerFilePath] string callingFile = null)
        {
            return callingFile;
        }

        public int GetCallerLineNumber([CallerLineNumber] int callingLineNum = 0)
        {
            return callingLineNum;
        }
    }
}
