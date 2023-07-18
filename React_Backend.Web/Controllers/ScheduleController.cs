using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Web.Helpers;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScheduleController : ControllerBase
    {
       
        public readonly IDoctorService _doctorService;
        public readonly IScheduleService _sheduleService;
        private readonly IdentityHelper _identifyHelper;
       

        public ScheduleController( IDoctorService doctorService, IScheduleService sheduleService,IdentityHelper identifyHelper)
        {
            _doctorService = doctorService;
            _sheduleService = sheduleService;
           _identifyHelper = identifyHelper
;        }

   
        /// <summary>
        ///  Using this method  doctor can add his/her schedule.
        /// </summary>
        /// <returns></returns>

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpPost(Name = "addschedule")]
        public Schedule Create([FromForm] Schedule model)
        {
            model.DoctorId = _identifyHelper.UserId;
            model.Status = true;
            var result = _sheduleService.Create(model);
            return result;

        }
    }
}