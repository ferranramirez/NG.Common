using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace NG.Common.Services.Token
{
    public class TokenService : ITokenService
    {
        public IEnumerable<Claim> GetClaims(StringValues authorizationHeader)
        {
            if (authorizationHeader.Any())
            {
                var token = AuthenticationHeaderValue.Parse(authorizationHeader);

                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(token.Parameter) as JwtSecurityToken;

                return tokenS.Claims;
            }
            return new List<Claim>();
        }
    }
}
