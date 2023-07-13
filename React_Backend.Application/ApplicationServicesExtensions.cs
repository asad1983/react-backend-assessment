


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React_Backend.Infrastructure;

namespace React_Backend.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configure)
        {
            //services.AddTransient<ICategoryManager, CategoryManager>();

            //services.AddAutoMapper(typeof(CategoryProfile));








            services.AddInfrastructureServices(configure);

            return services;
        }
    }
}
