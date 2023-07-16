using AutoMapper;
using React_Backend.Application.Interfaces;
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
        public IEnumerable<object> GetAll(string doctorId)
        {
            var data= _appointmentRepository.GetAll(doctorId);
            var list = _mapper.Map<IEnumerable<object>>(data);
            return list;
        }
    }
}
