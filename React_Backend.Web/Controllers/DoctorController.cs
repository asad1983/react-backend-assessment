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

        private readonly ILogger<DoctorController> _logger;

        public DoctorController(ILogger<DoctorController> logger, IDoctorService doctorService)
        {
            _logger = logger;
            _doctorService = doctorService;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpGet(Name = "doctorappointments")]
        
        public IEnumerable<AppointmentModel> Get()
        {
            return _doctorService.GetAll();
        }
    }
}