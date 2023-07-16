using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using React_Backend.Web.Helpers;
using React_Backend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace React_Backend.Web.Filters
{
    public class AppointmentDeleteFilter: IAsyncActionFilter
    {
        private readonly IdentityHelper _identifyHelper;
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentDeleteFilter(IAppointmentRepository appointmentRepository, IdentityHelper identifyHelper)
        {
            _appointmentRepository = appointmentRepository;
            _identifyHelper = identifyHelper;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // execute any code before the action executes
            var appointmentId = (string)context.ActionArguments["id"];
            if(!string.IsNullOrEmpty(appointmentId))
            {
                var userId = _identifyHelper.UserId;
                var appointmentModel = _appointmentRepository.Get(appointmentId);
                if (appointmentModel == null)
                {
                    context.Result = new BadRequestObjectResult("There is no appointment with this id");
                    return;
                }
                if(appointmentModel.PatientId == userId)
                {
                    var result = await next();
                }
                else
                {
                    context.Result = new BadRequestObjectResult("You don't have permission.");
                    return;
                }
                
            }
           
            // execute any code after the action executes
        }
    }
}
