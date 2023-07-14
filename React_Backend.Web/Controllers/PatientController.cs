using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
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
        public IActionResult Create(AppointmentModel model)
        {
             _service.CreateAppointment(model);
            return Ok("Created");
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpDelete]
        [Description("Delete a new Appointment")]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            _service.DeleteAppointment(id);
            return Ok("Deleted");
        }
    }
}