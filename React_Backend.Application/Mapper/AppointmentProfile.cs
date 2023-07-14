﻿
using AutoMapper;
using React_Backend.Application.Models;
using React_Backend.Domain.Entities;


namespace React_Backend.Application.Mapper
{
    public class AppointmentProfile: Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentModel>();
            CreateMap<AppointmentModel, Appointment>();

        }
    }
}
