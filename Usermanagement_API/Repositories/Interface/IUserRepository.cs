using Usermanagement_API.Models.Domain;
using Usermanagement_API.Models.DTO;

namespace Usermanagement_API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync(
            string? search = null,
            string? orderby = null,
            string? orderDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);

        Task<User?> GetUserByIdAsync(string id);

        Task<User?> UpdateAsync(User user);

        Task<User?> DeleteAsync(string id);

        Task<int> GetUsersCountAsync();
    }
}
