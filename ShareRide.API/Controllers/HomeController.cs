using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShareRide.API.Converter;
using ShareRide.API.Models;
using ShareRide.API.Services.Interface;

namespace ShareRide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("Admin")]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ConverterObjectsFromDataBase _converterObjects;

        public HomeController(IUserService userService, ConverterObjectsFromDataBase converterObjects)
        {
            _userService = userService;
            _converterObjects = converterObjects;
        }

        [HttpGet]
        public ActionResult GetHome()
        {
            return Ok("it work");
        }

        [HttpGet("AllUser")]
        public async Task<ActionResult> GetAllRoles()
        {
            return Ok(_converterObjects.ConvertUsersToListOfUserDtos(await _userService.GetAllUser()));
        }

        [HttpGet("User/{id:int}")]
        public async Task<ActionResult> GetUserById(int id)
        {
            return Ok(_converterObjects.ConvertUserToUserDto(await _userService.GetUserById(id)));
        }

        [HttpPost("search")]
        public async Task<ActionResult> FindAllAttribute([FromQuery(Name = "q")] string searchString)
        {
            return Ok(_converterObjects.ConvertUsersToListOfUserDtos(
                await _userService.FindAllByAttribute(searchString)));
        }
    }
}