using System;

namespace Programatica.Framework.Core.Adapter
{
    /// <summary>
    /// Interface for an authentication user adapter.
    /// </summary>
    public interface IAuthUserAdapter 
    {
        /// <summary>
        /// Gets the name of the authenticated user.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the password of the authenticated user.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// Gets the authentication type of the authenticated user.
        /// </summary>
        string AuthenticationType { get; }

        /// <summary>
        /// Gets the date and time of the last successful login of the authenticated user.
        /// </summary>
        DateTime LastLoginDateTime { get; }
    }
}
