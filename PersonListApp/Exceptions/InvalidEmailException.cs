using System;

namespace PersonListApp.Exceptions
{
    internal class InvalidEmailException : Exception
    {
        public InvalidEmailException(string message) : base(message)
        { }
        public InvalidEmailException(string message, Exception inner) : base(message, inner)
        { }
    }
}
