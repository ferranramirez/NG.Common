using Microsoft.AspNetCore.Builder;
using NG.Common.Presentation.Middleware;

namespace NG.Common.Presentation.Extensions
{
    public static class LogScopeMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogScopeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LogScopeMiddleware>();
        }
    }
}
