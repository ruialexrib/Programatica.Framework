using System;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Represents an implementation of <see cref="IAuthUserAdapter"/> that provides authentication user information.
    /// </summary>
    public class AuthUserAdapter : IAuthUserAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthUserAdapter"/> class.
        /// </summary>
        public AuthUserAdapter() : base()
        { }

        /// <summary>
        /// Gets the user name of the authenticated user.
        /// </summary>
        public string Name
        {
            get
            {
                return Environment.UserName;
            }
        }

        /// <summary>
        /// Gets the password of the authenticated user.
        /// </summary>
        public string Password
        { get; }

        /// <summary>
        /// Gets the type of authentication used for the authenticated user.
        /// </summary>
        public string AuthenticationType
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// Gets the last login date and time of the authenticated user.
        /// </summary>
        public DateTime LastLoginDateTime
        { get; }
    }
}
