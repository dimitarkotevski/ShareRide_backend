using ShareRide.API.Models;

namespace ShareRide.API.Services.Interface
{
    public interface IRoleService
    {
        public Task<Role> GetRoleById(int id);
    }
}
