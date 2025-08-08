using HealthChecks.UI.Client;
using MicroserviceEShopProject.BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Features;

namespace MicroserviceEShopProject.OrderingApi
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();

            #region ExceptionHandling
            services.AddExceptionHandler<ProblemExceptionHandler>();
            services.AddProblemDetails(opt =>
            {
                opt.CustomizeProblemDetails = context =>
                {
                    context.ProblemDetails.Instance =
                        $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";

                    context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

                    var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
                    context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);
                };
            });
            #endregion

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Database")!);

            return services;
        }

        public static WebApplication UseApiServices(this WebApplication app)
        {
            app.MapCarter();
            app.UseExceptionHandler();
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            return app;
        }
    }
}
