using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NG.Common.Library.Exceptions;
using System.Net;

namespace NG.Common.Library.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            var apiError = new ApiError();

            if (exception is NotGuiriBusinessException)
            {
                var ex = exception as NotGuiriBusinessException;

                filterContext.Exception = null;

                apiError.Message = ex.Message;

                apiError.ErrorCode = ex.ErrorCode;

                filterContext.HttpContext.Response.StatusCode = 330;

                _logger.LogWarning(
                    new EventId(0),
                    ex,
                    $"Application thrown error: {ex.Message}");
            }
            else if (exception is DbUpdateException)
            {
                filterContext.Exception = null;

                apiError.Message = exception.GetBaseException().Message;

                apiError.ErrorCode = 900;

                filterContext.HttpContext.Response.StatusCode = 331;

                _logger.LogWarning(
                    new EventId(0),
                    exception,
                    $"Database thrown error: {exception.Message}");

            }
            else
            {
                apiError.Message = filterContext.Exception.GetBaseException().Message;

                apiError.ErrorCode = (int)HttpStatusCode.InternalServerError;

                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogError(
                    new EventId(0),
                    exception,
                    $"Unhandled exception: {exception.Message}");
            }

            filterContext.Result = new JsonResult(apiError);
            filterContext.ExceptionHandled = true;
        }
    }
}
