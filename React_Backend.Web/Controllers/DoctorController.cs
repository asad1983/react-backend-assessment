using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Application.Services;
using React_Backend.Web.Filters;
using System.Data;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
       
        public readonly IDoctorService _doctorService;
        public readonly IScheduleService _sheduleService;

        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILogger<DoctorController> logger, IDoctorService doctorService, IScheduleService sheduleService)
        {
            _logger = logger;
            _doctorService = doctorService;
            _sheduleService = sheduleService;   
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpGet(Name = "doctorappointments")]
        
        public IEnumerable<object> Get()
        {
            return _doctorService.GetAll();
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpPost(Name = "addschedule")]
        public Schedule Create([FromForm] Schedule model)
        {
            return _sheduleService.Create(model);

        }
    }
}