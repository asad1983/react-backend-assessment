﻿using AutoMapper;
using React_Backend.Application.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Domain.Entities;
using React_Backend.Domain.Enums;
using React_Backend.Domain.Interfaces;


namespace React_Backend.Application.Services
{
    public class PatientService: IPatientService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IScheduleRepository _scheduleRepository;
        private readonly IMapper _mapper;
        private readonly IdentityHelper _identifyHelper;
        public PatientService(IAppointmentRepository appointmentRepository, IdentityHelper identifyHelper , IMapper mapper, IScheduleRepository scheduleRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _identifyHelper = identifyHelper;
            _scheduleRepository = scheduleRepository;
        }
        public IEnumerable<object> GetAll()
        {
            var patientId = _identifyHelper.UserId;
            var data = _appointmentRepository.GetAllPatientAppoints(patientId);
            var list = _mapper.Map<IEnumerable<object>>(data);
            return list;
        }
        public string CreateAppointment(AppointmentModel model)
        {
            var appointmentDto = _mapper.Map<Appointment>(model);
            appointmentDto.Status = EnumEntities.AppointmentStatus.Booked;
            appointmentDto.PatientId = _identifyHelper.UserId;
            appointmentDto.AppointmentId=Guid.NewGuid();
            var day=model.AppointmentDate.DayOfWeek;
            var doctorSchedules = _scheduleRepository.GetAll(model.DoctorId, day.ToString());
            if(doctorSchedules == null && doctorSchedules.Count()==0)
            {
                return "No appoointment is availeable";
            }
            else if (!ConfirmDoctorScheduleTime(doctorSchedules, model.StartTime,model.EndTime))
            {
                return "No appoointment is availeable";
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
        public string DeleteAppointment(string appointmentId)
        {
            var obj = _appointmentRepository.Get(appointmentId);
            _appointmentRepository.Delete(obj);
            return "Appointment Deleted";

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
    }
}
