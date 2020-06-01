using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using NG.Common.Services.Token;
using System;
using System.Linq;
using System.Security.Claims;

namespace NG.Common.Library.Filters
{
    public class AuthUserIdFromTokenAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var tokenService = filterContext.HttpContext.RequestServices.GetRequiredService<ITokenService>();

            var authorizationHeader = filterContext.HttpContext.Request.Headers["Authorization"].ToString();

            var tokenClaims = tokenService.GetClaims(authorizationHeader);

            var userId = tokenClaims.First(c => string.Equals(c.Type, ClaimTypes.NameIdentifier)).Value;

            filterContext.ActionArguments["AuthUserId"] = Guid.Parse(userId);
        }
    }
}
