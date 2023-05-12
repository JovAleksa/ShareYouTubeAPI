using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ShareYouTubeAPI.Models.Login;
using ShareYouTubeAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using NETCore.MailKit.Core;
using User.Manager.Service.Models;
using User.Manager.Service.Services;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ShareYouTubeAPI.Models.SingIn;
using System.Text.RegularExpressions;
//using User.Manager.Service.Services;

namespace ShareYouTubeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailServices _emailServices;

        public AuthenticationController(UserManager<IdentityUser> _userManager, 
            IConfiguration configuration, RoleManager<IdentityRole> roleManager, 
            IEmailServices emailServices, SignInManager<IdentityUser> signInManager)
        {
            this._userManager = _userManager;
            _configuration = configuration;
            this.roleManager = roleManager; 
            _emailServices = emailServices;   
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("login-2FA")]
        public async Task<IActionResult> LoginWithOTP(string code, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            var signIn =await _signInManager.TwoFactorSignInAsync("Email", code, false, false);
            if(signIn.Succeeded) 
            {
                if (user != null )
                {

                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role));
                    }


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

            }
            return NotFound("Wrong code");

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Login parameters invalid.");
            }
            var user =await _userManager.FindByNameAsync(model.Username);

            if (user.TwoFactorEnabled)
            {
                await _signInManager.SignOutAsync();
                await _signInManager.PasswordSignInAsync(user,model.Password, false, true);
                var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
                var message = new Message(new string[] { user.Email! }, "OTP confirmation", token);
                _emailServices.SendEmail(message);
                return Ok($"Whe have sent OTP to your email {user.Email}!");

            }
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                var userRoles = await _userManager.GetRolesAsync(user);
                foreach(var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
               

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
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Registration parameters invalid.");
            }

            var userExists = await _userManager.FindByNameAsync(model.Username);
            
            if (userExists != null)
            {
                return BadRequest("User already exists");
            }
            var mailExists = await _userManager.FindByEmailAsync(model.Email);
            if (mailExists != null)
            {
                return BadRequest("Email already exists");
            }


            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            if (await roleManager.RoleExistsAsync(role))
            {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    return BadRequest("Validation failed! Please check user details and try again.");
                }

                //insert role

                await _userManager.AddToRoleAsync(user, role);

                //add email required task

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confiramtionLink=Url.Action(nameof(ConfirmEmail),"Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Confirmation email link", confiramtionLink!);
                _emailServices.SendEmail(message);
                return Ok($"User is created and confirmation email to { user.Email } is sent!");
            }
            else
            {
                return BadRequest("This role is not exist!");
            }
            
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _userManager.ConfirmEmailAsync(user, token);
                if(result.Succeeded)
                {
                    return Ok("Email verified successfuly!");
                }
            }
            return BadRequest("This user doesnot exist!");
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword([Required] string  email)
        {
            var user = await _userManager.FindByEmailAsync (email);
            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var frogotPasswordLink = Url.Action("ResetPassword", "Authentication", new { token, email = user.Email }, Request.Scheme);
                var message = new Message(new string[] { user.Email! }, "Forgot password  link", frogotPasswordLink!);
                _emailServices.SendEmail(message);
                return Ok($"Request for reset password is send to {user.Email}.");
            }
            return NotFound("Can`t send request to email.");

        }

        [HttpGet("reset-password")]

        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            var model = new ResetPassword { Token = token, Email = email };
            return Ok(new 
            {
                model,
            });

        }


        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            if (user != null)
            {
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
               if (!resetPasswordResult.Succeeded)
                {
                    foreach(var error in resetPasswordResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return Ok(ModelState);
                }
               return Ok("New password is created!");
                
            }
            return BadRequest("There  is  be some error!");

        }
    }
}
