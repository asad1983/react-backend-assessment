using AutoMapper;
using React_Backend.Application.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Domain.Interfaces;

namespace React_Backend.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IdentityHelper _identifyHelper;
        private readonly IMapper _mapper;
        public DoctorService(IAppointmentRepository appointmentRepository, IdentityHelper identifyHelper ,IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _identifyHelper = identifyHelper;
            _mapper= mapper;
        }
        public IEnumerable<object> GetAll()
        {
            var data= _appointmentRepository.GetAll(_identifyHelper.UserId);
            var list = _mapper.Map<IEnumerable<object>>(data);
            return list;
        }
    }
}
