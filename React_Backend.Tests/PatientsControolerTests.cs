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
        _patientService.Setup(m => m.DeleteAppointment(Constant.AppointmentId)).Returns("Appointment Deleted");

    }

    [Test]
    public  void Patient_Apppointment_Create()
    {
        // Arrange
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

        // Assert
        PatientController controller = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = controller.Create(model);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.AreEqual("Created", okResult.Value);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [Test]
    public void Patient_Apppointments_Get()
    {
        // Act
        PatientController controller = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = controller.Get();
        var okResult = result as OkObjectResult;
        var appointments = okResult.Value as List<object>;

        // Assert
        Assert.AreEqual(1, appointments.Count());
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [Test]
    public void Patient_Apppointment_Delete()
    {

        // Act
        PatientController controller = new PatientController(_patientService.Object, _identityHelper.Object);
        var result = controller.Delete(Constant.AppointmentId);
        var okResult = result as OkObjectResult;

        // Assert
        Assert.AreEqual("Appointment Deleted", okResult.Value);
        Assert.AreEqual(200, okResult.StatusCode);
    }


}
