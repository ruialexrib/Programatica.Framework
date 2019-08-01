using System;

namespace Programatica.Framework.Core.Adapter
{
    public interface IAuthUserAdapter : IObject
    {
        string Name { get; }
        string Password { get; }
        string AuthenticationType { get; }
        DateTime LastLoginDateTime { get; }
    }
}
