using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NG.Common.Library.Extensions
{
    public static class JwtServiceExtensions
    {
        public static void AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration tokenSection)
        {
            var authKey = tokenSection.GetValue<string>("AuthKey");
            var validAudience = tokenSection.GetValue<string>("ValidAudience");
            var ValidIssuer = string.Concat("https://securetoken.google.com/", validAudience);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer("Firebase", options =>
                {
                    options.Authority = ValidIssuer;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = ValidIssuer,
                        ValidateAudience = true,
                        ValidAudience = validAudience,
                        ValidateLifetime = true
                    };
                })
                .AddJwtBearer("Bearer", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(authKey)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });


            services
                .AddAuthorization(options =>
                {
                    options.DefaultPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Bearer", "Firebase")
                    .Build();
                })
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme);

        }
    }
}
