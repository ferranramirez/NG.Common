using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NG.Common.Services.Token;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NG.Common.Library.Middleware
{
    public class LogScopeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LogScopeMiddleware> _logger;

        public LogScopeMiddleware(
            RequestDelegate next,
            ITokenService tokenService,
            ILogger<LogScopeMiddleware> logger)
        {
            _next = next;
            _tokenService = tokenService;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var tokenClaims = _tokenService.GetClaims(context.Request.Headers["Authorization"]);
            string email = tokenClaims.Any() ?
                    tokenClaims.First(c => string.Equals(c.Type, ClaimTypes.Email)).Value : "anonymous";
            string role = tokenClaims.Any() ?
                    tokenClaims.First(c => string.Equals(c.Type, ClaimTypes.Role)).Value : "none";

            using (_logger.BeginScope(email))
            using (_logger.BeginScope(role))
            {
                await _next(context);
            }
        }
    }
}
