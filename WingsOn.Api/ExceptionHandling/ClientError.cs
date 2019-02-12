using System.Diagnostics.CodeAnalysis;

namespace WingsOn.Api.ExceptionHandling
{
    [ExcludeFromCodeCoverage]
    public class ClientError
    {
        public string Message { get; set; }
    }
}