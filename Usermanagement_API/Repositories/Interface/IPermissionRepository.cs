using Usermanagement_API.Models.Domain;

namespace Usermanagement_API.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetPermissionsAsync();

        Task<Permission?> GetPermissionByIdAsync(string permissionId);

        Task<UserPermission> GetPermissionByUserIdAsync(string userId);
    }
}
