using Microsoft.AspNetCore.Builder;
using NG.Common.Library.Middleware;

namespace NG.Common.Library.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLogScopeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogScopeMiddleware>();
        }

        public static IApplicationBuilder UseErrorDisplayMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorDisplayMiddleware>();
        }
    }
}
