using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NG.Common.Library.Middleware
{
    public class ErrorDisplayMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorDisplayMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            if (context.Request.Path.Value.Contains("/errors"))
            {
                var errors = configuration.GetSection("Errors");

                var report = errors.GetChildren().Select(x => $"{x.Key}\n{{\n\tErrorCode: { x.GetSection("ErrorCode").Value },\n\tMessage: { x.GetSection("Message").Value }\n}}");

                await context.Response.WriteAsync(string.Join(",\n", report));
            }
            else
            {
                await _next(context);
            }
        }
    }
}
