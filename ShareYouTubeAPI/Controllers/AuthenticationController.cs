using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareYouTubeAPI.Models.Login;
using ShareYouTubeAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShareYouTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager; 
        private readonly IConfiguration _configuration;

        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            _configuration = configuration;
            this.roleManager = roleManager; 
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Login parameters invalid.");
            }
            var user = userManager.FindByNameAsync(model.Username).GetAwaiter().GetResult();
            if (user != null && userManager.CheckPasswordAsync(user, model.Password).GetAwaiter().GetResult())
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    expires: DateTime.Now.AddHours(2),      // token valid for 2 hours
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new TokenDTO()
                {
                    Username = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    Email = user.Email
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model, string role)
//        public IActionResult Register([FromBody] RegistrationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Registration parameters invalid.");
            }

            var userExists = userManager.FindByNameAsync(model.Username).GetAwaiter().GetResult();
            var mailExists = userManager.FindByEmailAsync(model.Email).GetAwaiter().GetResult();
            if (userExists != null)
            {
               //  return StatusCode(StatusCodes.Status403Forbidden,new Response { Status = "Error", MessageProcessingHandler = "User already exists" });
                return BadRequest("User already exists");
            }
            if (mailExists != null)
            {
                return BadRequest("Email already exists");
            }


            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            if (await roleManager.RoleExistsAsync(role))
            {
                var result = userManager.CreateAsync(user, model.Password).GetAwaiter().GetResult();
                if (!result.Succeeded)
                {
                    return BadRequest("Validation failed! Please check user details and try again.");
                }

                //insert role

                await userManager.AddToRoleAsync(user, role);
                return Ok("User is create!");
            }
            else
            {
                return BadRequest("This role is not exist!");
            }
            
        }

    }
}
