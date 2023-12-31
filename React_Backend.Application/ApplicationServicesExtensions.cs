﻿


using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Mapper;
using React_Backend.Application.Services;
using React_Backend.Infrastructure;


namespace React_Backend.Application
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configure)
        {
            services.AddTransient<ISpecialiasationService, SpecialiasationService>();
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<IScheduleService, ScheduleService>();

            services.AddAutoMapper(typeof(AppointmentProfile));
            services.AddAutoMapper(typeof(ScheduleProfile));
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            

            services.AddInfrastructureServices(configure);
            return services;
        }
    }
}
