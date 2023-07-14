using AutoMapper;
using React_Backend.Application.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;


namespace React_Backend.Application.Services
{
    public class PatientService: IPatientService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;
        private readonly IdentityHelper _identifyHelper;
        public PatientService(IAppointmentRepository appointmentRepository, IdentityHelper identifyHelper ,IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _identifyHelper = identifyHelper;
        }
        public IEnumerable<object> GetAll()
        {
            var patientId = _identifyHelper.UserId;
            var data = _appointmentRepository.GetAllPatientAppoints(patientId);
            var list = _mapper.Map<IEnumerable<object>>(data);
            return list;
        }
        public void CreateAppointment(AppointmentModel model)
        {
            var appointmentDto = _mapper.Map<Appointment>(model);
            appointmentDto.AppointmentDateTime = DateTime.Now.AddHours(3);
            appointmentDto.PatientId = _identifyHelper.UserId;
            appointmentDto.AppointmentId=Guid.NewGuid();
            var result=_appointmentRepository.Create(appointmentDto);
        }
        public string DeleteAppointment(string appointmentId)
        {
            var obj = _appointmentRepository.Get(appointmentId);
            var patientId= _identifyHelper.UserId;
            if(patientId==obj.PatientId)
            {
                _appointmentRepository.Delete(obj);
                return "Appointment Deleted";
            }
            return "";
            
        }
    }
}
