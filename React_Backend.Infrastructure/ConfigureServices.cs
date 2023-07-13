
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React_Backend.Domain.Entities;
using React_Backend.Infrastructure.Context;

namespace React_Backend.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {


            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //// For Identity
            services.AddIdentityCore<IdentityUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            return services;
        }
    }
}
