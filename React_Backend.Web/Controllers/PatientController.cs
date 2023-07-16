using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Web.Filters;
using React_Backend.Web.Validation;
using System.ComponentModel;
using React_Backend.Application.Enums;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
       
        public readonly IPatientService _service;
        private readonly IdentityHelper _identifyHelper;
        public PatientController(IPatientService service, IdentityHelper identifyHelper)
        {
            _service = service;
            _identifyHelper = identifyHelper;
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpGet(Name = "patientsappointments")]
        public IEnumerable<object> Get()
        {
            return _service.GetAll(_identifyHelper.UserId);
        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        [HttpPost(Name = "createappointment")]
        [Description("Create a new Appointment")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Create([FromForm]AppointmentModel model)
        {
             model.PatientId = _identifyHelper.UserId;
             model.Status = EnumEntities.AppointmentStatus.Booked;
             model.AppointmentId = Guid.NewGuid();
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