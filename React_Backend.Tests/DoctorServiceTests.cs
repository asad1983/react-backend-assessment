using Moq;
using React_Backend.Application.Services;
using AutoMapper;
using React_Backend.Domain.Interfaces;
using React_Backend.Application.Models;


namespace React_Backend.Tests;

public class DoctorServiceTests
{

    private readonly  Mock<IAppointmentRepository> _appointmentRepository;
    private readonly Mock<IMapper>   _mapper;

    public DoctorServiceTests()
    {
        _appointmentRepository=new Mock<IAppointmentRepository>();
        _mapper=new Mock<IMapper>();

    }
    [SetUp]
    public void Setup()
    {

        var now = DateTime.Now.AddDays(3);
        now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        var formatDateToday = new DateTime(now.Year, now.Month, now.Day);
        var formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var startTime = TimeOnly.FromTimeSpan(now.AddHours(3).TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(now.AddHours(4).TimeOfDay);

        
        var appointmentList = new List<object>();

        appointmentList.Add(new
        {
            Detail = "test",
            Title = "test",
            Patient = "Patient",
            StartTime = startTime,
            EndTime = endTime,
            Id = Constant.AppointmentId
        });

        var filter = new Domain.Entities.AppointmentFilter
        {
            DoctorId = Constant.DoctorId,
            Date = formatDateOnly
        };

        _mapper.Setup(mock => mock.Map<Domain.Entities.AppointmentFilter>(It.IsAny<AppointmentFilter>())).Returns(filter);
        _appointmentRepository.Setup(m => m.GetAll(filter)).Returns(appointmentList);
      
    }

    [Test]
    public  void Doctor_Apppointments_Get()
    {

        var now = DateTime.Now.AddDays(3);
        now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        var formatDateToday = new DateTime(now.Year, now.Month, now.Day);
        var formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var filter = new AppointmentFilter
        {
            DoctorId = Constant.DoctorId,
            Date = formatDateOnly
        };

        DoctorService _service = new DoctorService(_appointmentRepository.Object,_mapper.Object);
        var result = _service.GetAll(filter);
        Assert.AreEqual(1, result.Count());
    }

    
}



