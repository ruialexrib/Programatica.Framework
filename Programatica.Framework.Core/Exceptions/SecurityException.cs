using System;

namespace Programatica.Framework.Core.Exceptions
{
    /// <summary>
    /// Represents an exception related to security.
    /// </summary>
    public class SecurityException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        public SecurityException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public SecurityException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception.</param>
        public SecurityException(string message, Exception inner) : base(message, inner) { }
    }

}
