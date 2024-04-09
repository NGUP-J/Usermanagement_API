using Usermanagement_API.Models.Domain;

namespace Usermanagement_API.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesAsync();

        Task<Role?> GetRoleByIdAsync(string roleId);
    }
}
