using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Domain.Entities;
using React_Backend.Web.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using React_Backend.Application.Enums;

namespace React_Backend.Tests;

public class PatientsControolerTests
{
    private readonly Mock<IPatientService> _patientService;
    private readonly Mock<IHttpContextAccessor> _iHttpContextAccessor;
    private readonly Mock<IdentityHelper> _identityHelper;

    DateTime dateToday;
    DateTime formatDateToday;
    DateOnly formatDateOnly;
    public PatientsControolerTests()
    {
        _patientService = new Mock<IPatientService>();
        _iHttpContextAccessor = new Mock<IHttpContextAccessor>();
        var claimPriciple  = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, Constant.PatientName),
            new Claim("Id", Constant.PatientId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        }, "Basic"));
        var context = new DefaultHttpContext()
        {
            User= claimPriciple
        };
        _iHttpContextAccessor.Setup(x => x.HttpContext).Returns(context);
        _identityHelper = new Mock<IdentityHelper>(_iHttpContextAccessor.Object);
    }

    [SetUp]
    public void Setup()
    {
        var list = new List<object>();
        dateToday = DateTime.Now.AddDays(5);
        formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
        formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var startTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay);
        var endTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay);
        list.Add(new
        {
            Detail = "Test Appointment",
            Title = "Test Appointment Detail",
            Doctor = "Doctor",
            StartTime = startTime,
            EndTime = endTime

        });
        _patientService.Setup(m => m.GetAll(Constant.PatientId)).Returns(list);
        //var user = new AppUser() { UserName = Constant.PatientName, Id = Constant.PatientId };
        //var claims = new List<Claim>()
        //    {
        //        new Claim(ClaimTypes.Name, user.UserName),
        //        new Claim("Id", user.Id),
        //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    };

        //claims.Add(new Claim(ClaimTypes.Role, Constant.Patient));
        //var identity = new ClaimsIdentity(claims, "Test");
        //var claimsPrincipal = new ClaimsPrincipal(identity);

        //var mockPrincipal = new Mock<IPrincipal>();
        //mockPrincipal.Setup(x => x.Identity).Returns(identity);
        //mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

        //var mockHttpContext = new Mock<HttpContext>();
        //mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal);

    }

    [Test]
    public  void Patient_Apppointment_Create()
    {

        var dateToday = DateTime.Now.AddDays(5);
        DateTime formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
        DateOnly formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var list=new List<Appointment>();
        var model = new Application.Models.AppointmentModel()
        {
           
            AppointmentDate = formatDateOnly,
            DoctorId = Constant.DoctorId,
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay),
            Status= EnumEntities.AppointmentStatus.Booked
        };

        _patientService.Setup(p => p.CreateAppointment(model)).Returns("Created");

        PatientController doct = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = doct.Create(model);
        var okResult = result as OkObjectResult;
        Assert.AreEqual("Created", okResult.Value);
    }

    [Test]
    public void Patient_Apppointment_Get()
    {
        
        PatientController doct = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = doct.Get();
        Assert.AreEqual(1, result.Count());
    }

    [Test]
    public void Patient_Apppointment_Delete()
    {

        PatientController doct = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = doct.Delete(new Guid().ToString());
        var okResult = result as OkObjectResult;
        Assert.AreEqual("Deleted", okResult.Value);
    }

    //[Test]
    //public void Patient_Apppointment_Create_Conflics()
    //{
    //    var user = new AppUser() { UserName = "doctor", Id = "b74ddd14-6340-4840-95c2-db12554843e5" };
    //    Mock<UserManager<AppUser>> userMgr = GetMockUserManager();
    //    userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);


    //    var dateToday = DateTime.Now.AddDays(5);
    //    DateTime formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
    //    DateOnly formatDateOnly = DateOnly.FromDateTime(formatDateToday);
    //    var list = new List<Appointment>();
    //    var startTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay);
    //    var endTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay);
    //    list.Add(new Appointment()
    //    {
    //        Id = 2,
    //        AppointmentId = new Guid("AE21D696-4EB0-4311-9369-0E90E9547EDE"),
    //        AppointmentDate = formatDateOnly,
    //        DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
    //        StartTime = startTime,
    //        EndTime = endTime,
    //        PatientId= "b74ddd14-6340-4840-95c2-db12554843e6"
    //    });
    //    var model = new Application.Models.AppointmentModel()
    //    {

    //        AppointmentDate = formatDateOnly,
    //        DoctorId = "b74ddd14-6340-4840-95c2-db12554843e5",
    //        StartTime = startTime,
    //        EndTime = endTime
    //    };
    //    _patientService.Setup(p => p.GetAll("b74ddd14-6340-4840-95c2-db12554843e6")).Returns(list);
    //    _patientService.Setup(p => p.CreateAppointment(model)).Returns("Created");
    //    PatientController doct = new PatientController(_patientService.Object, _identityHelper.Object);
    //    var result = doct.Create(model);
    //    var okResult = result as OkObjectResult;
    //    Assert.AreEqual("Created", okResult.Value);
    //}


}