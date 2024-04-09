using Microsoft.EntityFrameworkCore;
using Usermanagement_API.Data;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Repositories.Implementation
{
    public class PermissionRepository: IPermissionRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public PermissionRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return await dbcontext.Permissions.ToListAsync();
        }

        public async Task<Permission?> GetPermissionByIdAsync(string permissionId)
        {
            return await dbcontext.Permissions.FirstOrDefaultAsync(p => p.permissionId == permissionId);
        }

        public async Task<UserPermission> GetPermissionByUserIdAsync(string userId)
        {
            var existUserPermissions = await dbcontext.UserPermissions.Where(up => up.userId == userId).FirstOrDefaultAsync();

            if (existUserPermissions != null)
            {
                return existUserPermissions;
            }
            return null;
        }
    }
}
