using System;

namespace Programatica.Framework.Core.Adapter
{
    public class AuthUserAdapter : IAuthUserAdapter
    {
        public AuthUserAdapter() : base()
        { }

        public string Name
        {
            get
            {
                return Environment.UserName;
            }
        }

        public string Password
        { get; }

        public string AuthenticationType
        {
            get
            {
                return "";
            }
        }

        public DateTime LastLoginDateTime
        { get; }
    }
}
