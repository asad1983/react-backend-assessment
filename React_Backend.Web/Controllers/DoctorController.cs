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
        //private readonly ILogger<DoctorController> _logger;

        public DoctorController( IDoctorService doctorService, IScheduleService sheduleService,IdentityHelper identifyHelper)
        {
            _doctorService = doctorService;
            _sheduleService = sheduleService;
           _identifyHelper = identifyHelper
;        }

        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpGet(Name = "doctorappointments")]
        
        public IEnumerable<object> Get()
        {
            var result = _doctorService.GetAll(_identifyHelper.UserId);
            return result;
        }
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpPost(Name = "addschedule")]
        public Schedule Create([FromForm] Schedule model)
        {
            model.DoctorId = _identifyHelper.UserId;
            var result = _sheduleService.Create(model);
            return result;

        }
    }
}