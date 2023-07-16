using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Services;
using React_Backend.Domain.Entities;
using React_Backend.Web.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using React_Backend.Domain.Interfaces;
using System.Numerics;
using React_Backend.Application.Models;

namespace React_Backend.Tests;

public class PatientServiceTests
{

    private readonly  Mock<IAppointmentRepository> _appointmentRepository;
    private readonly  Mock<IScheduleRepository> _scheduleRepository;
    private readonly Mock<IMapper>   _mapper;

    DateTime dateToday;
    DateTime formatDateToday;
    DateOnly formatDateOnly;
    public PatientServiceTests()
    {
        _appointmentRepository=new Mock<IAppointmentRepository>();
        _scheduleRepository=new Mock<IScheduleRepository>();
        _mapper=new Mock<IMapper>();

    }
    [SetUp]
    public void Setup()
    {

        dateToday = DateTime.Now.AddDays(5);
        var list = new List<object>();
        formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
        formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var startTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay);
        list.Add(new
        {
            Detail = "test",
            Title = "test",
            Doctor = "Doctor",
            StartTime = startTime,
            EndTime = endTime
            
        });
        var scheduleList = new List<Domain.Entities.Schedule>();
        var now = DateTime.Now;
        now=new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        scheduleList.Add(new Domain.Entities.Schedule
        {
            Day="Monday",
            DoctorId= "b74ddd14-6340-4840-95c2-db12554843e5",
            Status=true,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
        });
        scheduleList.Add(new Domain.Entities.Schedule
        {
            Day = "TuseDay",
            DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
            Status = true,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
        });
       scheduleList.Add(new Domain.Entities.Schedule
             {
                 Day = "WednesDay",
                 DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
                 Status = true,
                 StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
                 EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
             });
         var date = DateTime.Now.AddDays(3);
        _appointmentRepository.Setup(m => m.GetAllPatientAppoints("b74ddd14-6340-4840-95c2-db12554843e6")).Returns(list);
        _scheduleRepository.Setup(m => m.GetAll("b74ddd14-6340-4840-95c2-db12554843e5", date.DayOfWeek.ToString())).Returns(scheduleList);

    }

    [Test]
    public  void Patient_Apppointment_Get()
    {
       
        //_mapper.Setup(m => m.Map<Appointment, object>(It.IsAny<Appointment>())).Returns((Appointment src) => new object());
        PatientService _service = new PatientService(_appointmentRepository.Object,_mapper.Object,_scheduleRepository.Object);
        var result = _service.GetAll("b74ddd14-6340-4840-95c2-db12554843e6");
        Assert.AreEqual(1, result.Count());
    }

    [Test]
    public void Patient_Apppointment_Create_Rejected()
    {

        var model = new Appointment()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay),
            PatientId= "b74ddd14-6340-4840-95c2-db12554843e6",
        };

        _appointmentRepository.Setup(p => p.Create(model)).Returns(model);
        PatientService _service = new PatientService(_appointmentRepository.Object, _mapper.Object, _scheduleRepository.Object);
        var result = _service.CreateAppointment(new AppointmentModel()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay),
            PatientId = "b74ddd14-6340-4840-95c2-db12554843e6",
        });
        Assert.AreEqual("No appoointment is availeable", result);
    }

    [Test]
    public void Patient_Apppointment_Create()
    {
        var now = DateTime.Now.AddDays(3);
        now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        var formatDateToday = new DateTime(now.Year, now.Month, now.Day);
        var formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var model = new Appointment()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(10).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(11).TimeOfDay),
            PatientId = "b74ddd14-6340-4840-95c2-db12554843e6",
        };

        _appointmentRepository.Setup(p => p.Create(model)).Returns(model);
        PatientService _service = new PatientService(_appointmentRepository.Object, _mapper.Object, _scheduleRepository.Object);
        var result = _service.CreateAppointment(new AppointmentModel()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(10).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(11).TimeOfDay),
            PatientId = "b74ddd14-6340-4840-95c2-db12554843e6",
        });
        Assert.AreEqual("Created", result);
    }

}



