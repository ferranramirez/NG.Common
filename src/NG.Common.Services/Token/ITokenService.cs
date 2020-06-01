using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Security.Claims;

namespace NG.Common.Services.Token
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaims(StringValues authorizationHeader);
    }
}
