using Microsoft.Extensions.Caching.Memory;
using ShareRide.API.Models;
using ShareRide.API.Repository;
using ShareRide.API.Services.Interface;

namespace ShareRide.API.Services.Impl
{
    public class RoleService :IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository,IMemoryCache memoryCache)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role> GetRoleById(int id)
        {
            return await _roleRepository.GetRoleById(id);
        }
    }
}
