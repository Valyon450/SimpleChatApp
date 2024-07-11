using DataAccess;
using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.DI
{
    public static class DependencyConfiguration
    {
        public static IServiceCollection ConfigureDependency(this IServiceCollection services, IConfiguration configuration)
        {
            // Database context
            services.AddDbContext<ISimpleChatDbContext, SimpleChatDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }
    }
}
