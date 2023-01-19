using Microsoft.EntityFrameworkCore;
using ShareRide.API.DataContext;
using ShareRide.API.Models;

namespace ShareRide.API.Repository
{
    public class RoleRepository
    {
        private readonly ShareRideDbContext _context;

        public RoleRepository(ShareRideDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _context.Roles.FirstAsync(r => r.Id == id);
        }
    }
}
