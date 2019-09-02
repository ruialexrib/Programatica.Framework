using System;
using System.Runtime.CompilerServices;

namespace Programatica.Framework.Core
{
    public interface IObject
    {
        Guid InstanceSystemId { get; set; }
        DateTime InstanceDateTime { get; set; }

        string GetCallerMemberName([CallerMemberName] string callingMember = null);
        string GetCallerFilePath([CallerFilePath] string callingFile = null);
        int GetCallerLineNumber([CallerLineNumber] int callingLineNum = 0);
    }
}
