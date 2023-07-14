
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;
using React_Backend.Infrastructure.Context;
using React_Backend.Infrastructure.Repositories;

namespace React_Backend.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            services.AddTransient<IScheduleRepository, ScheduleRepository>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            //// For Identity
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            return services;
        }
    }
}
