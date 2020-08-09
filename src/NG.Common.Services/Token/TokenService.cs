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

                var decodedToken = DecodeToken(token.Parameter);

                return decodedToken.Claims;
            }
            return new List<Claim>();
        }

        public JwtSecurityToken DecodeToken(string token)
        {
            return new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
        }
    }
}
