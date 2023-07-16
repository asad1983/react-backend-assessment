using AutoMapper;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Domain.Interfaces;


namespace React_Backend.Application.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        public ScheduleService(IScheduleRepository scheduleRepository ,IMapper mapper)
        {
            _scheduleRepository = scheduleRepository;
            _mapper = mapper;
        }
        public Schedule Create(Schedule model)
        {
            var scheduleDto = _mapper.Map<Domain.Entities.Schedule>(model);
            var result=_scheduleRepository.Create(scheduleDto);
            return _mapper.Map<Schedule>(result);
        }
    }
}
