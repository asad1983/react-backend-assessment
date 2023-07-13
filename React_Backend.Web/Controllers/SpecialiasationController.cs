using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using React_Backend.Application.Interfaces;
using React_Backend.Application.Models;
using System.Data;

namespace React_Backend.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpecialiasationController : ControllerBase
    {
       
        public readonly ISpecialiasationService _specialiasationService;

        private readonly ILogger<SpecialiasationController> _logger;

        public SpecialiasationController(ILogger<SpecialiasationController> logger, ISpecialiasationService specialiasationService)
        {
            _logger = logger;
            _specialiasationService = specialiasationService;
        }

       // [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        [HttpGet(Name = "all")]
        public IEnumerable<SpecialiasationModel> Get()
        {
            return _specialiasationService.GetAll();
        }
    }
}