using ShareRide.API.Models;
using ShareRide.API.Models.Dto;

namespace ShareRide.API.Converter
{
    public class UserDtoToUser
    {
        public User ConvertUserDtoToUser(UserRegisterDto userRegisterDto, string password,Role role)
        {
            return new User {
                Email = userRegisterDto.Email,
                Username = userRegisterDto.Username,
                Password = password,
                RoleId = userRegisterDto.RoleId,
                Role= role,
                dateCreated = DateTime.Now
            };
        }
    }
}