using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceEShopProject.Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conString = configuration.GetConnectionString("Database");

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(conString));

            return services;
        }
    }
}
