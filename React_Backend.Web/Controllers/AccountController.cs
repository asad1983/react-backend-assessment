using React_Backend.Application.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace React_Backend.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [EnableCors("AllowOrigin")]
    public class AccountController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        //private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            this.userManager = userManager;
            this.roleManager = roleManager;

        }
        //public AccountController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        //{
        //    this.userManager = userManager;
        //    this.roleManager = roleManager;
        //    _configuration = configuration;
        //}

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{
        //    var user = await userManager.FindByNameAsync(model.Username);
        //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        //    {
        //        var userRoles = await userManager.GetRolesAsync(user);

        //        var authClaims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, user.UserName),
        //            new Claim("Id", user.Id),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //        };

        //        foreach (var userRole in userRoles)
        //        {
        //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        //        }

        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

        //        var token = new JwtSecurityToken(
        //            issuer: _configuration["JWT:ValidIssuer"],
        //            audience: _configuration["JWT:ValidAudience"],
        //            expires: DateTime.Now.AddHours(3),
        //            claims: authClaims,
        //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo
        //        });
        //    }
        //    return Unauthorized();
        //}

        //[HttpPost]
        //[Route("register")]
        //public async Task<IActionResult> Register([FromBody] RegisterModel model)
        //{
        //    var userExists = await userManager.FindByNameAsync(model.Username);
        //    if (userExists != null)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

        //    Domain.Entities.ApplicationUser user = new Domain.Entities.ApplicationUser()
        //    {
        //        Email = model.Email,
        //        SecurityStamp = Guid.NewGuid().ToString(),
        //        UserName = model.Username,
        //        FirstName = model.FirstName,
        //        LastName = model.LastName,
        //        Phone  = model.Phone,
        //        Address = model.Address,
        //    };
        //    var result = await userManager.CreateAsync(user, model.Password);
        //    if (!result.Succeeded)
        //        return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Doctor))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Doctor));
        //    if (!await roleManager.RoleExistsAsync(UserRoles.Patient))
        //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Patient));

        //    if (model.isDoctor)
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Doctor);
        //    }
        //    else
        //    {
        //        await userManager.AddToRoleAsync(user, UserRoles.Patient);
        //    }

        //    return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        //}

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
        [HttpGet(Name = "GetWeatherForecastTest")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }


    
}
