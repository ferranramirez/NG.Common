using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NG.Auth.Presentation.WebAPI
{
    public static class HealthCheckExtensions
    {

        public static void AddHealthCheckMiddleware(
            this IServiceCollection services,
            IConfiguration config)
        {
            services.AddHealthChecks()
                    .AddSqlServer(config.GetConnectionString("NotGuiriDb"));
            services.AddHealthChecksUI(setup => setup.AddHealthCheckEndpoint("API HealthCheck", "/health"))
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
