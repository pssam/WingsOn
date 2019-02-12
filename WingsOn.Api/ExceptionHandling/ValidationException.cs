using System;
using System.Diagnostics.CodeAnalysis;

namespace WingsOn.Api.ExceptionHandling
{
    [ExcludeFromCodeCoverage]
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