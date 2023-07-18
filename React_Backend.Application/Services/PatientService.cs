using AutoMapper;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Domain.Entities;
using React_Backend.Domain.Interfaces;


namespace React_Backend.Application.Services
{
    public class PatientService: IPatientService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;

        public PatientService(IAppointmentRepository appointmentRepository, IMapper mapper, IScheduleRepository scheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _scheduleRepository = scheduleRepository;
        }
        public IEnumerable<object> GetAll(string patientId)
        {
           // var patientId = _identifyHelper.UserId;
            var data = _appointmentRepository.GetAllPatientAppoints(patientId);
            //var list = _mapper.Map<IEnumerable<object>>(data);
            return data;
        }
        public string CreateAppointment(AppointmentModel model)
        {
            var appointmentDto = _mapper.Map<Appointment>(model);
            //appointmentDto.Status = EnumEntities.AppointmentStatus.Booked;
            //appointmentDto.PatientId = _identifyHelper.UserId;
            
            var day=model.AppointmentDate.DayOfWeek;
            var doctorSchedules = _scheduleRepository.GetAll(model.DoctorId, day.ToString());
            if(doctorSchedules == null && doctorSchedules.Count()==0)
            {
                return "Doctor is not availeable on selected day";
            }
            else if (!ConfirmDoctorScheduleTime(doctorSchedules, model.StartTime,model.EndTime))
            {
                return "Doctor is not availeable at this time";
            }
            else if (!ConfirmAppointmentAvaileablity(model.DoctorId,model.AppointmentDate,model.StartTime))
            {
                return "No appoointment is availeable";
            }
            else
            {
                var result=_appointmentRepository.Create(appointmentDto);
                return "Created";
                
            }
           
        }
        public string UpdateAppointment(EditAppointmentModel model)
        {

            var day = model.AppointmentDate.DayOfWeek;
            var doctorSchedules = _scheduleRepository.GetAll(model.DoctorId, day.ToString());
            if (doctorSchedules == null && doctorSchedules.Count() == 0)
            {
                return "This Doctor is not availeable for selected day";
            }
            else if (!ConfirmDoctorScheduleTime(doctorSchedules, model.StartTime, model.EndTime))
            {
                return "This Doctor is not availeable for selected time";
            }
            else
            {
                var filter = new Domain.Entities.AppointmentFilter
                {
                    DoctorId = model.DoctorId,
                    Date = model.AppointmentDate,
                };
                var doctorappointmentsList = _appointmentRepository.GetAll(filter);
                var filterAppointmentsList = doctorappointmentsList.Where(x => x.Id != model.AppointmentId);
                var appointmentDto = _appointmentRepository.Get(model.AppointmentId.ToString());

                if (appointmentDto.AppointmentDate!=model.AppointmentDate && !ConfirmAppointmentAvaileablity(model.DoctorId, model.AppointmentDate, model.StartTime))
                {
                    return "No appoointment is availeable";
                }
                else if(appointmentDto.AppointmentDate == model.AppointmentDate && !CheckAppointmentOverLap(filterAppointmentsList, model.StartTime))
                {
                    return "Your appointment is overlap. You can not update your appointment";
                }
                else
                {
                    appointmentDto.StartTime=model.StartTime;
                    appointmentDto.EndTime=model.EndTime;
                    appointmentDto.AppointmentDate=model.AppointmentDate;
                    //_appointmentRepository.Update(appointmentDto);

                }
                return "Updated";

            }

        }
        public string DeleteAppointment(string appointmentId)
        {
            var obj = _appointmentRepository.Get(appointmentId);
            if (obj != null)
            {
                _appointmentRepository.Delete(obj);
                return "Appointment Deleted";
            }
            return "Invalid appointment";

        }


        private bool ConfirmDoctorScheduleTime(IEnumerable<Domain.Entities.Schedule> schedules,TimeOnly startTime,TimeOnly endTime)
        {

            foreach (var schedule in schedules)
            {
                if(schedule.StartTime<=startTime && schedule.EndTime >= endTime)
                {
                    return true;
                }
            }
            return false;
        }

        private bool ConfirmAppointmentAvaileablity(string doctorId,DateOnly appointmentDate,TimeOnly appointmentStartTime)
        {
            var restults = _appointmentRepository.GetAll(doctorId, appointmentDate);
            if (restults.Count() == 0)
            {
                return true;
            }
            foreach (var restult in restults)
            {
                if (restult.StartTime >= appointmentStartTime && restult.EndTime <= appointmentStartTime)
                {
                    return false;
                }
            }
            return true;
        }


        private bool CheckAppointmentOverLap(IEnumerable<Domain.ViewModels.AppointmentViewModel> appointments, TimeOnly startTime)
        {

            foreach (var schedule in appointments)
            {
                if (startTime>=schedule.StartTime && startTime <= schedule.EndTime)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
