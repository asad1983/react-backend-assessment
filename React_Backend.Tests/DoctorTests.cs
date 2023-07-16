using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Services;
using React_Backend.Domain.Entities;
using React_Backend.Web.Controllers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace React_Backend.Tests;

public class DoctorTests
{
    private readonly Mock<IDoctorService> _doctorService;
    private readonly Mock<IScheduleService> _sheduleService;
    private readonly Mock<IdentityHelper> _identityHelper;
    //public Mock<IDoctorService> mock = new Mock<IDoctorService>();
    public DoctorTests()
    {
        _doctorService = new Mock<IDoctorService>();
        _sheduleService = new Mock<IScheduleService>();
        _identityHelper = new Mock<IdentityHelper>();
    }
    [SetUp]
    public void Setup()
    {
        var user = new AppUser() { UserName = "doctor", Id = "b74ddd14-6340-4840-95c2-db12554843e5" };
        var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        claims.Add(new Claim(ClaimTypes.Role, "Doctor"));
        var identity = new ClaimsIdentity(claims, "Test");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var mockPrincipal = new Mock<IPrincipal>();
        mockPrincipal.Setup(x => x.Identity).Returns(identity);
        mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);

        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal);
        
    }

    [Test]
    public  void Test1()
    {
        var user = new AppUser() { UserName = "doctor", Id = "b74ddd14-6340-4840-95c2-db12554843e5" };
        Mock<UserManager<AppUser>> userMgr = GetMockUserManager();
        userMgr.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        
       
        var dateToday = DateTime.Now.AddDays(5);
        DateTime formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
        DateOnly formatDateOnly = DateOnly.FromDateTime(formatDateToday);
        var list=new List<Appointment>();
        list.Add(new Appointment()
        {
            Id = 2,
            AppointmentId =new Guid("AE21D696-4EB0-4311-9369-0E90E9547EDE"),
            AppointmentDate = formatDateOnly,
            DoctorId= "b74ddd14-6340-4840-95c2-db12554843e5",
            StartTime = TimeOnly.FromTimeSpan(dateToday.AddHours(3).TimeOfDay),
            EndTime = TimeOnly.FromTimeSpan(dateToday.AddHours(4).TimeOfDay)
        });
        _doctorService.Setup(p => p.GetAll("b74ddd14-6340-4840-95c2-db12554843e5")).Returns(list);
        DoctorController doct = new DoctorController(_doctorService.Object, _sheduleService.Object, _identityHelper.Object);
        var result = doct.Get();
        Assert.True(list.Equals(result));
    }


    public Mock<UserManager<AppUser>> GetMockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<AppUser>>();
        return new Mock<UserManager<AppUser>>(
            userStoreMock.Object, null, null, null, null, null, null, null, null);
    }
}


public class AppUser
{
    public string Id { get; set; }
    public string UserName { get; set; }
}