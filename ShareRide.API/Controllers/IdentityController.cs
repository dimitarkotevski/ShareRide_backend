using Microsoft.AspNetCore.Mvc;
using ShareRide.API.Models;
using System.ComponentModel.DataAnnotations;
using ShareRide.API.Converter;
using ShareRide.API.Dto.RegisterAndLoginDto;
using ShareRide.API.Services.Interface;

namespace ShareRide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ConverterObjectsFromDataBase _converterObjects;

        public IdentityController(IUserService userService, ConverterObjectsFromDataBase converterObjects)
        {
            _userService = userService;
            _converterObjects = converterObjects;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody, Required] UserRegisterDto userDto,
            [FromHeader, Required] string password)
        {
            return Ok(_converterObjects.ConvertUserToUserDto(await _userService.RegisterUser(userDto, password)));
        }
        [HttpPost]
        [Route("confirm-registration")]
        public async Task<ActionResult> ConfirmRegister([FromHeader] string code)
        {
            return Ok(_converterObjects.ConvertUserToUserDto(await _userService.ConfirmRegisterUser(code)));
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userDto)
        {
            return Ok(await _userService.LoginUser(userDto));
        }
    }
}