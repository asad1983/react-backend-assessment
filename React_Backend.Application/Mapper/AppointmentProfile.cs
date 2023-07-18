
using AutoMapper;
using React_Backend.Application.Enums;
using React_Backend.Application.Models;
using React_Backend.Application.Models.ViewModels;

namespace React_Backend.Application.Mapper
{
    public class AppointmentProfile: Profile
    {
        public AppointmentProfile()
        {
            CreateMap<object, AppointmentModel>();
            CreateMap<AppointmentModel, Domain.Entities.Appointment>();

            CreateMap<AppointmentViewModel, Domain.ViewModels.AppointmentViewModel>();
            CreateMap<Domain.ViewModels.AppointmentViewModel, AppointmentViewModel>();

            CreateMap<AppointmentFilter, Domain.Entities.AppointmentFilter>();
            CreateMap<Domain.Entities.AppointmentFilter, AppointmentFilter>();


            CreateMap<EnumEntities.AppointmentStatus,Domain.Enums.EnumEntities.AppointmentStatus>();
            CreateMap<Domain.Enums.EnumEntities.AppointmentStatus, EnumEntities.AppointmentStatus>();

        }
    }
}
