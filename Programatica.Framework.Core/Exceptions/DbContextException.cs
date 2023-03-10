using System;

namespace Programatica.Framework.Core.Exceptions
{
    public class DbContextException : Exception
    {
        public DbContextException() { }

        public DbContextException(string message) : base(message) { }

        public DbContextException(string message, Exception inner) : base(message, inner) { }
    }
}
