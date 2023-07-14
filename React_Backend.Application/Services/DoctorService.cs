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
        public DoctorService(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper= mapper;
        }
        public IEnumerable<AppointmentModel> GetAll()
        {
            var data= _appointmentRepository.GetAll();
            var list = _mapper.Map<IEnumerable<AppointmentModel>>(data);
            return list;
        }
    }
}
