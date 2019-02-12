using System.Diagnostics.CodeAnalysis;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WingsOn.Api.ExceptionHandling
{
    [ExcludeFromCodeCoverage]
    public class ExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            _logger.LogError(exception, $"Internal error happened {context.HttpContext.Request.Path}{context.HttpContext.Request.QueryString}");
            var clientError = new ClientError { Message = "Something went wrong" };

            if (exception is ValidationException validationException)
            {
                _logger.LogError($"Internal details: {validationException.Details}");
                clientError.Message = validationException.Message;
            }

            var result = new JsonResult(clientError);
            result.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Result = result;
        }
    }
}