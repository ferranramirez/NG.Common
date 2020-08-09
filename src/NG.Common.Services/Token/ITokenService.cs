using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NG.Common.Services.Token
{
    public interface ITokenService
    {
        IEnumerable<Claim> GetClaims(StringValues authorizationHeader);
        JwtSecurityToken DecodeToken(string token);
    }
}
