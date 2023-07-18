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
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
    public class PatientController : ControllerBase
    {
       
        public readonly IPatientService _service;
        private readonly IdentityHelper _identifyHelper;
        public PatientController(IPatientService service, IdentityHelper identifyHelper)
        {
            _service = service;
            _identifyHelper = identifyHelper;
        }

        /// <summary>
        ///  Using this method we can see patient appointments.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "patientsappointments")]
        public IActionResult Get()
        {
            return Ok(_service.GetAll(_identifyHelper.UserId));
        }

        /// <summary>
        ///  Using this method patient can book an appointments.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        ///  Using this method patient can delete an appointments.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Description("Delete a new Appointment")]
        [Route("{id}")]
        [ServiceFilter(typeof(AppointmentDeleteFilter), Order = 1)]
        public IActionResult Delete(string id)
        {
            var reulst = _service.DeleteAppointment(id);
            return Ok(reulst);
        }

        /// <summary>
        ///  Using this method patient can update an appointments.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Put([FromForm]EditAppointmentModel model)
        {
            var reulst=_service.UpdateAppointment(model);
            return Ok(reulst);
        }
    }
}