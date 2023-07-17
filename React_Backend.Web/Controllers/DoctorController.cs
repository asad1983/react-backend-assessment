using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;

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
        ///  Usig this method we can see doctor appointments. By default it will show current date appoints.
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpPost(Name = "doctorappointments")]
        
        public IEnumerable<object> Get(AppointmentFilter model)
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
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        //[HttpPost(Name = "addschedule")]
        //public Schedule Create([FromForm] Schedule model)
        //{
        //    model.DoctorId = _identifyHelper.UserId;
        //    var result = _sheduleService.Create(model);
        //    return result;

        //}
    }
}