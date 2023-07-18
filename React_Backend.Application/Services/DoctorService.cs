﻿using AutoMapper;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Application.Models.ViewModels;
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
        public IEnumerable<AppointmentViewModel> GetAll(AppointmentFilter filter)
        {
            var filterModel = _mapper.Map<Domain.Entities.AppointmentFilter>(filter);
            var appoitments= _appointmentRepository.GetAll(filterModel);
            var appoitmentsData = _mapper.Map<IEnumerable<AppointmentViewModel>>(appoitments);
            return appoitmentsData;
        }
    }
}
