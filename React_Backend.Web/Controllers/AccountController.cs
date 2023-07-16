using React_Backend.Application.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using React_Backend.Domain.Entities;
using React_Backend.Web.Validation;

namespace React_Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("AllowOrigin")]
    public class AccountController : ControllerBase
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        //public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    this.userManager = userManager;
        //    this.roleManager = roleManager;
        //    _configuration = configuration;
        //}

        [HttpPost]
        [Route("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Login([FromBody] Application.Models.LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))] 
        [Route("register")]
        public async Task<IActionResult> Register([FromForm] Application.Models.RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Application.Models.Response { Status = "Error", Message = "User already exists!" });

            var user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.Phone,
                Address = model.Address,
                EmailConfirmed=true,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Application.Models.Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            //if (!await _roleManager.RoleExistsAsync(Application.Models.UserRoles.Doctor))
            //    await _roleManager.CreateAsync(new IdentityRole(Application.Models.UserRoles.Doctor));
            //if (!await _roleManager.RoleExistsAsync(Application.Models.UserRoles.Patient))
            //    await _roleManager.CreateAsync(new IdentityRole(Application.Models.UserRoles.Patient));

            if (model.isDoctor)
            {
                await _userManager.AddToRoleAsync(user, Application.Models.UserRoles.Doctor);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, Application.Models.UserRoles.Patient);
            }

            return Ok(new Application.Models.Response { Status = "Success", Message = "User created successfully!" });
        }

        //[HttpPost]
        //[Route("register-admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        //{
        //    try
        //    {
        //        var userExists = await userManager.FindByNameAsync(model.Username);
        //        if (userExists != null)
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //        Domain.Entities.ApplicationUser user = new Domain.Entities.ApplicationUser()
        //        {
        //            Email = model.Email,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //            UserName = model.Username,
        //            FirstName = model.FirstName,
        //            LastName = model.LastName,
        //            Address= model.Address,
        //        };
        //        var result = await userManager.CreateAsync(user, model.Password);
        //        if (!result.Succeeded)
        //            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        //        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        //        if (!await roleManager.RoleExistsAsync(UserRoles.User))
        //            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        //        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        //        {
        //            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        //        }

        //        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //    } catch(Exception ex)
        //    {
        //        return Ok(new Response { Status = "Error", Message = ex.Message });
        //    }

        //}

    }


    
}
