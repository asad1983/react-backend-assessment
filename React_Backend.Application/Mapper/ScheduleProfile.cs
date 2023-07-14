
using AutoMapper;
using React_Backend.Application.Models;



namespace React_Backend.Application.Mapper
{
    public class ScheduleProfile : Profile
    {
        public ScheduleProfile()
        {
            CreateMap<Domain.Entities.Schedule, Schedule>();
            CreateMap<Schedule, Domain.Entities.Schedule>();

        }
    }
}
