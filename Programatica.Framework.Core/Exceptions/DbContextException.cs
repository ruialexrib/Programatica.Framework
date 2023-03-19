using System;

namespace Programatica.Framework.Core.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when an error occurs in a DbContext.
    /// </summary>
    public class DbContextException : Exception
    {
        /// <summary>
        /// Default constructor for DbContextException.
        /// </summary>
        public DbContextException() { }

        /// <summary>
        /// Initializes a new instance of the DbContextException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DbContextException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the DbContextException class with a specified error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public DbContextException(string message, Exception inner) : base(message, inner) { }
    }
}
