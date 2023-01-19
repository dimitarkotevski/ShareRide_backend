using Microsoft.EntityFrameworkCore;
using ShareRide.API.DataContext;
using ShareRide.API.Dto.RegisterAndLoginDto;
using ShareRide.API.Models;


namespace ShareRide.API.Repository;
public class UserRepository
{
    private readonly ShareRideDbContext _context;

    public UserRepository(ShareRideDbContext context)
    {
        _context = context;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
    public void Save(User user)
    {
        _context.Add(user);
    }

    public async Task<User> FindByDto(UserLoginDto userLoginDto)
    {
        return await _context.Users.FirstOrDefaultAsync(u =>
            u.Email.Equals(userLoginDto.Username) && u.Password.Equals(userLoginDto.Password)) ?? throw new System.Exception();
    }

    public async Task<List<User>> FindAll()
    {
        return await _context.Users.ToListAsync();
    }

    public Task<User> FindById(int id)
    {
        return _context.Users.FirstAsync(c => c.Id.Equals(id));
    }

    public async Task<List<User>> FindAllAllWithSearchString(string searchString)
    {
        return await _context.Users.Where(c =>
                c.Email.Contains(searchString) || c.Username.Contains(searchString))
            .ToListAsync();
    }

    public Task<User> FindByVerificationCode(string code)
    {
        return _context.Users.FirstAsync(c => c.VerificationCode.Code.Equals(code));
    }

    public async Task<List<User>> FindByAttribute(string searchString)
    {
        return await _context.Users.Where(c =>
                c.Email.Contains(searchString) || c.Username.Contains(searchString))
            .ToListAsync();
    }

    public User FindUserByEmailOrUsername(string searchString)
    {
        return _context.Users.FirstOrDefault(c =>
                c.Email.Contains(searchString) || c.Username.Contains(searchString));
    }
    public Task<List<User>> FindAllByAttribute(string searchString)
    {
        return _context.Users.Where(c => c.IsEnabled).ToListAsync();
    }
}