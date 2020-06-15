using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NG.Common.Library.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void AddHealthCheckMiddleware(
            this IServiceCollection services,
            IConfiguration config)
        {
            var baseUrl = config.GetSection("Urls").GetValue<string>("Base") ?? "localhost:80";
            var hcName = string.Concat(config.GetSection("Documentation").GetValue<string>("Title"), "HealthCheck");
            services.AddHealthChecks()
                    .AddNpgSql(config.GetConnectionString("NotGuiriDb"));
            services.AddHealthChecksUI(setup => setup.AddHealthCheckEndpoint(hcName, string.Concat(baseUrl, "/health")))
                    .AddInMemoryStorage();
        }

        public static void UseHealthCheckMiddleware(
            this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseHealthChecksUI((Options options) => options.UIPath = "/health-ui");
        }
    }
}
