using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ShareRide.API.Config;
using ShareRide.API.Converter;
using ShareRide.API.Dto.RegisterAndLoginDto;
using ShareRide.API.Exception;
using ShareRide.API.Models;
using ShareRide.API.Repository;
using ShareRide.API.Security.Hashing;
using ShareRide.API.Services.Interface;

namespace ShareRide.API.Services.Impl;
public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly UserRepository _userRepository;
    private readonly IRoleService _roleService;
    private readonly ConverterObjectsFromDataBase _userDtoToUser;
    private readonly HashingPassword _hashingPassword;
    private readonly EmailConfig _emailConfig;
    private readonly RandomVerificationCode _randomVCode;
    private readonly VerificationCodeRepository _verificationCodeRepository;

    public UserService(UserRepository userRepository,
        ConverterObjectsFromDataBase userDtoToUser,
        IRoleService roleService,
        HashingPassword hashingPassword, IConfiguration configuration, EmailConfig emailConfig, RandomVerificationCode randomVCode, VerificationCodeRepository verificationCodeRepository = null)
    {
        _userRepository = userRepository;
        _userDtoToUser = userDtoToUser;
        _roleService = roleService;
        _hashingPassword = hashingPassword;
        _configuration = configuration;
        _emailConfig = emailConfig;
        _randomVCode = randomVCode;
        _verificationCodeRepository = verificationCodeRepository;
    }

    public async Task<User> RegisterUser(UserRegisterDto userDto, string password)
    {
        var role = await _roleService.GetRoleById(userDto.RoleId);
        string code = _randomVCode.RandomString(10);
        VerificationCode verification=new VerificationCode { 
            Code= code ,
            DateCreated= DateTime.Now,
            DateExpire= DateTime.Now.AddMinutes(10),
        };
        var user = _userDtoToUser.ConverRegistertUserDtoToUser(userDto, _hashingPassword.Encrypt(password), role,verification);
        _emailConfig.sendRegisterMail(user.Email, code);
        _userRepository.Save(user);
        await _userRepository.SaveChanges();
        return user;
    }
    public async Task<User> ConfirmRegisterUser(string code)
    {
        User user=await _userRepository.FindByVerificationCode(code);
        //user.IsEnabled = true;
        //user.VerificationCode = null;
        VerificationCode verificationCode=await _verificationCodeRepository.FindByCode(code);
        _verificationCodeRepository.DeleteByCode(verificationCode);
        return null;//user;
    }

    public async Task<string> LoginUser(UserLoginDto userLoginDto)
    {
        userLoginDto.Password = _hashingPassword.Encrypt(userLoginDto.Password);

        
            User user = await _userRepository.FindByDto(userLoginDto);
            return CreateToken(await  _userRepository.FindByDto(userLoginDto));
       
    }

    public User GetUserByEmailOrUsername(string searchString)
    {
        try
        {
            return _userRepository.FindUserByEmailOrUsername(searchString);
        }
        catch
        {
            throw new UserNotFoundException("User not found");
        }
    }


    public async Task<User> GetUserById(int id)
    {
        try
        {
            return await _userRepository.FindById(id);
        }
        catch
        {
            throw new UserIdNotExistException("User with id '" + id + "' doesn't exist");
        }
    }


    public async Task<List<User>> GetAllUser()
    {
        return await _userRepository.FindAll();
    }

    public Task<List<User>> FindAllByAttribute(string searchString)
    {
        try
        {
            return _userRepository.FindAllByAttribute(searchString);
        }
        catch
        {
            throw new UserNotFoundException("User(s) not found");
        }
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.Name),
            new(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString())
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