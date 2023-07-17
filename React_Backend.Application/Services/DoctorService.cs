using AutoMapper;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Domain.Interfaces;

namespace React_Backend.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        public DoctorService(IAppointmentRepository appointmentRepository,IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper= mapper;
        }
        public IEnumerable<object> GetAll(AppointmentFilter model)
        {
            var filterModel = _mapper.Map<Domain.Entities.AppointmentFilter>(model);
            var data= _appointmentRepository.GetAll(filterModel);
            //var list = _mapper.Map<IEnumerable<object>>(data);
            return data;
        }
    }
}
