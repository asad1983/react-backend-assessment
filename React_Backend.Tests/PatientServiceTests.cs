using Moq;
using React_Backend.Application.Services;
using React_Backend.Domain.Entities;
using AutoMapper;
using React_Backend.Domain.Interfaces;
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
        var appointmentList = new List<object>();
        formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
        formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var startTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay);
        appointmentList.Add(new
        {
            Detail = "test",
            Title = "test",
            Doctor = "Doctor",
            StartTime = startTime,
            EndTime = endTime,
            Id= Constant.AppointmentId
        });


        var scheduleList = new List<Domain.Entities.Schedule>();
        var now = DateTime.Now;
        now=new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        scheduleList.Add(new Domain.Entities.Schedule
        {
            Day="Monday",
            DoctorId= Constant.DoctorId,
            Status=true,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
        });
        scheduleList.Add(new Domain.Entities.Schedule
        {
            Day = "TuseDay",
            DoctorId = Constant.DoctorId,
            Status = true,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
        });
       scheduleList.Add(new Domain.Entities.Schedule
             {
                 Day = "WednesDay",
                 DoctorId = Constant.DoctorId,
                 Status = true,
                 StartTime = TimeOnly.FromTimeSpan(now.AddHours(9).TimeOfDay),
                 EndTime = TimeOnly.FromTimeSpan(now.AddHours(20).TimeOfDay),
             });
         var date = DateTime.Now.AddDays(3);
        _appointmentRepository.Setup(m => m.GetAllPatientAppoints(Constant.PatientId)).Returns(appointmentList);
        _appointmentRepository.Setup(m => m.Get(Constant.AppointmentId)).Returns(new Appointment
        {
            Detail = "test",
            Title = "test",
            StartTime = startTime,
            EndTime = endTime,
            AppointmentId =new Guid(Constant.AppointmentId)
        });
        _scheduleRepository.Setup(m => m.GetAll(Constant.DoctorId, date.DayOfWeek.ToString())).Returns(scheduleList);

    }

    [Test]
    public  void Patient_Apppointment_Get()
    {
       
        //_mapper.Setup(m => m.Map<Appointment, object>(It.IsAny<Appointment>())).Returns((Appointment src) => new object());
        PatientService _service = new PatientService(_appointmentRepository.Object,_mapper.Object,_scheduleRepository.Object);
        var result = _service.GetAll(Constant.PatientId);
        Assert.AreEqual(1, result.Count());
    }

    [Test]
    public void Patient_Apppointment_Create_Rejected()
    {

        var model = new Appointment()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = Constant.DoctorId,
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay),
            PatientId= Constant.PatientId,
        };

        _appointmentRepository.Setup(p => p.Create(model)).Returns(model);
        PatientService _service = new PatientService(_appointmentRepository.Object, _mapper.Object, _scheduleRepository.Object);

        var result = _service.CreateAppointment(new AppointmentModel()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = Constant.DoctorId,
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay),
            PatientId = Constant.PatientId,
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
            DoctorId = Constant.DoctorId,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(10).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(11).TimeOfDay),
            PatientId = Constant.PatientId,
        };

        _appointmentRepository.Setup(p => p.Create(model)).Returns(model);
        PatientService _service = new PatientService(_appointmentRepository.Object, _mapper.Object, _scheduleRepository.Object);
        var result = _service.CreateAppointment(new AppointmentModel()
        {

            AppointmentDate = formatDateOnly,
            DoctorId = Constant.DoctorId,
            StartTime = TimeOnly.FromTimeSpan(now.AddHours(10).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(now.AddHours(11).TimeOfDay),
            PatientId = Constant.PatientId,
        });
        Assert.AreEqual("Created", result);
    }
    [Test]
    public void Patient_Apppointment_Delete()
    {
        PatientService _service = new PatientService(_appointmentRepository.Object, _mapper.Object, _scheduleRepository.Object);
        var result = _service.DeleteAppointment(Constant.AppointmentId);

        Assert.AreEqual("Appointment Deleted", result);
    }
}



