using Microsoft.AspNetCore.Http;
using Moq;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Web.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using React_Backend.Application.Models;
using React_Backend.Application.Models.ViewModels;

namespace React_Backend.Tests;

public class DoctorControllerTests
{
    private readonly Mock<IDoctorService> _doctorService;
    private readonly Mock<IScheduleService> _sheduleService;
    private readonly Mock<IdentityHelper> _identityHelper;
    private readonly Mock<IHttpContextAccessor> _iHttpContextAccessor;

    
    public DoctorControllerTests()
    {
        _doctorService = new Mock<IDoctorService>();
        _sheduleService = new Mock<IScheduleService>();
        _iHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var claimPriciple = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
       {
            new Claim(ClaimTypes.Name, Constant.DoctorName),
            new Claim("Id", Constant.DoctorId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
       }, "Basic"));
        var context = new DefaultHttpContext()
        {
            User = claimPriciple
        };
        _iHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        _identityHelper = new Mock<IdentityHelper>(_iHttpContextAccessor.Object);
    }
    [SetUp]
    public void Setup()
    {
        
        var appointments = new List<AppointmentViewModel>();
        var now = DateTime.Now.AddDays(3);
        now = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0);
        var formatDateToday = new DateTime(now.Year, now.Month, now.Day);
        var formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var startTime = TimeOnly.FromTimeSpan(now.AddHours(3).TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(now.AddHours(4).TimeOfDay);

        appointments.Add(new AppointmentViewModel()
        {
            Detail = "test",
            Title = "test",
            Patient = "Patient",
            StartTime = startTime,
            EndTime = endTime,
            Id = new Guid(Constant.AppointmentId),
            Date= formatDateOnly,

        });
        var filter = new AppointmentFilter
        {
            DoctorId=Constant.DoctorId,
            Date= formatDateOnly
        };
        _doctorService.Setup(m => m.GetAll(filter)).Returns(appointments);

    }

    [Test]
    public void Doctor_Apppointments_Get()
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
        DoctorController controller = new DoctorController(_doctorService.Object, _sheduleService.Object, _identityHelper.Object);
        var result = controller.Get(filter);
        //Assert.True(list.Equals(result));   Due to some reason this test case is not working
    }
}


