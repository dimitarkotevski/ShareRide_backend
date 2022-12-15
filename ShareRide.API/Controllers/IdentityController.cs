﻿using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareRide.API.DataContext;
using ShareRide.API.Hashing;
using ShareRide.API.Models;
using ShareRide.API.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ShareRide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly ShareRideDbContext _context;
        private readonly IConfiguration _configuration;
        public IdentityController(ShareRideDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserRegisterDto userDto,[FromHeader] [Required] string password)
        {
            var user = new User
            {
                Email = userDto.Email,
                Username = userDto.Username,
                Password = HashingPassword.Encrypt(password)
            };
            _context.Add(user);
            _context.SaveChanges();
            return Ok(user);
        }
        
        [Route("login")]
        public async Task<ActionResult> Login( UserLoginDto userDto)
        {
            User user =await _context.Users.FirstAsync(u => 
                u.Email == userDto.UserName && 
                u.Password== HashingPassword.Encrypt( userDto.Password ));
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(CreateToken(user));
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, 
                    Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));        
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);           
            var tokenDescriptor = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value, _configuration.GetSection("Jwt:Issuer").Value, claims, 
                expires: DateTime.Now.AddDays(10), 
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}