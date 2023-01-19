using ShareRide.API.Dto;
using ShareRide.API.Dto.RegisterAndLoginDto;
using ShareRide.API.Models;

namespace ShareRide.API.Converter
{
    public class ConverterObjectsFromDataBase
    {
        public User ConverRegistertUserDtoToUser(UserRegisterDto userRegisterDto, string password,Role role,VerificationCode verification)
        {
            return new User {
                Email = userRegisterDto.Email,
                Username = userRegisterDto.Username,
                Password = password,
                RoleId = userRegisterDto.RoleId,
                Role = role,
                DateCreated = DateTime.Now,
                IsEnabled = false,
                VerificationCode= verification
            };
        }

        public UserDto ConvertUserToUserDto(User user)
        {
            UserDto userDto = new UserDto
            {
                Id= user.Id,
                Email = user.Email,
                Username= user.Username,
                DateCreated= DateTime.Now,
                Address= user.Address,
                PhoneNumber= user.PhoneNumber,
                Role=user.Role.Name
            };
            return userDto;
        }

        public List<UserDto> ConvertUsersToListOfUserDtos(List<User> users)
        {
            return  users.ConvertAll(ConvertUserToUserDto);
        }
        
    }
}