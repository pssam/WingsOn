using System;

namespace WingsOn.Api.ExceptionHandling
{
    public class ValidationException : Exception
    {
        public string Details { get; }

        public ValidationException(string message): base(message)
        {
            // Do nothing.
        }

        public ValidationException(string message, string innerDetails): base(message)
        {
            Details = innerDetails;
        }
    }
}