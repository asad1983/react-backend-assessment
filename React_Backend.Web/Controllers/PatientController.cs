using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Web.Filters;
using React_Backend.Web.Validation;
using System.ComponentModel;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
       
        public readonly IPatientService _service;

        private readonly ILogger<PatientController> _logger;

        public PatientController(ILogger<PatientController> logger, IPatientService service)
        {
            _logger = logger;
            _service = service;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpGet(Name = "patientsappointments")]
        public IEnumerable<object> Get()
        {
            return _service.GetAll();
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpPost(Name = "createappointment")]
        [Description("Create a new Appointment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromForm]AppointmentModel model)
        {
             return Ok(_service.CreateAppointment(model));
             //return Ok(model);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpDelete]
        [Description("Delete a new Appointment")]
        [Route("{id}")]
        [ServiceFilter(typeof(AppointmentDeleteFilter), Order = 1)]
        public IActionResult Delete(string id)
        {
            _service.DeleteAppointment(id);
            return Ok("Deleted");
        }
    }
}