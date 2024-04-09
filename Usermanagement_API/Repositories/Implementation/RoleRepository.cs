using Microsoft.EntityFrameworkCore;
using Usermanagement_API.Data;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Repositories.Implementation
{
    public class RoleRepository: IRoleRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public RoleRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await dbcontext.Roles.ToListAsync();
        }

        public async Task<Role?> GetRoleByIdAsync(string roleId)
        {
            return await dbcontext.Roles.FirstOrDefaultAsync(r => r.roleId == roleId);
        }
    }
}
