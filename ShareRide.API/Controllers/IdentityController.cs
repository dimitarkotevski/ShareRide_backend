using Microsoft.AspNetCore.Mvc;
using ShareRide.API.DataContext;
using ShareRide.API.Models;
using ShareRide.API.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShareRide.API.Security.Hashing;
using ShareRide.API.Services.Interface;

namespace ShareRide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public IdentityController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody,Required] UserRegisterDto userDto,[FromHeader,Required] string password)
        {
            return   Ok(await _userService.RegisterUser(userDto,password));
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userDto)
        {
            User user = await _userService.LoginUser(userDto);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(CreateToken(user));
        }

        [HttpGet("AllUser")]
        public ActionResult GetAllRoles()
        {
            return Ok(_userService.GetAllUser());
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role,user.Role.Name),
                new (ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));        
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
            var tokenDescriptor = new JwtSecurityToken(
                _configuration.GetSection("Jwt:Issuer").Value, 
                _configuration.GetSection("Jwt:Issuer").Value, 
                    claims, 
                    expires: DateTime.Now.AddDays(10), 
                    signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
