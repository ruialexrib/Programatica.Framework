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

        /// <summary>
        /// Allows you to obtain the method or property name of the caller to the method.
        /// </summary>
        public string GetCallerMemberName([CallerMemberName] string callingMember = null)
        {
            return callingMember;
        }

        /// <summary>
        /// Allows you to obtain the full path of the source file that contains the caller.
        /// This is the file path at the time of compile.
        /// </summary>
        public string GetCallerFilePath([CallerFilePath] string callingFile = null)
        {
            return callingFile;
        }

        /// <summary>
        /// Allows you to obtain the line number in the source file at which the method is called.
        /// </summary>
        public int GetCallerLineNumber([CallerLineNumber] int callingLineNum = 0)
        {
            return callingLineNum;
        }
    }
}
