using ShareRide.API.Dto.RegisterAndLoginDto;
using ShareRide.API.Models;

namespace ShareRide.API.Services.Interface
{
    public interface IUserService
    {
        public Task<User> RegisterUser(UserRegisterDto userDto, string password);
        public Task<string> LoginUser(UserLoginDto userLoginDto);
        public User GetUserByEmailOrUsername(string searchString);
        public Task<User> GetUserById(int id);
        public Task<List<User>> GetAllUser();
        public Task<List<User>> FindAllByAttribute(string searchString);
        public Task<User> ConfirmRegisterUser(string code);
    }
}