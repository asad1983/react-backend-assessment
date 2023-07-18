using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using React_Backend.Application.Models.ViewModels;
using React_Backend.Web.Filters;
using System.ComponentModel;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
       
        public readonly IDoctorService _doctorService;
        public readonly IScheduleService _sheduleService;
        private readonly IdentityHelper _identifyHelper;
       

        public DoctorController( IDoctorService doctorService, IScheduleService sheduleService,IdentityHelper identifyHelper)
        {
            _doctorService = doctorService;
            _sheduleService = sheduleService;
           _identifyHelper = identifyHelper
;        }

   
        /// <summary>
        ///  Using this method we can see doctor appointments. By default it will show current date appoints.
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpPost(Name = "doctorappointments")]
        
        public IEnumerable<AppointmentViewModel> Get(AppointmentFilter model)
        {
            if (model.Date == null)
            {
                var dateToday = DateTime.Now;
                DateTime formatDateToday = new DateTime(dateToday.Year, dateToday.Month, dateToday.Day);
                DateOnly formatDateOnly = DateOnly.FromDateTime(formatDateToday);
                model.Date = formatDateOnly;
            }
            model.DoctorId = _identifyHelper.UserId;
            var result = _doctorService.GetAll(model);
            return result;
        }



        /// <summary>
        ///  Using this method doctor can delete an appointments.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Description("Delete Appointment")]
        [Route("{id}")]
        [AllowAnonymous]
        public IActionResult Delete(string id)
        {
            var reulst = _doctorService.Delete(id);
            return Ok(reulst);
        }

    }
}