using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Usermanagement_API.Data;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Repositories.Implementation
{
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationDbContext dbcontext;
        public UserRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbcontext.Users.AddAsync(user);
            await dbcontext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(
            string? search = null,
            string? orderBy = null,
            string? orderDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100)
        {
            // Query
            var users= dbcontext.Users.Include(u => u.UserPermissions).AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users.Where(u => u.firstName.Contains(search) || u.lastName.Contains(search) || u.email.Contains(search));
            }

            // Order
            if (!string.IsNullOrWhiteSpace(orderBy) && !string.IsNullOrWhiteSpace(orderDirection))
            {
                if (string.Equals(orderBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isAsc = string.Equals(orderDirection, "asc", StringComparison.OrdinalIgnoreCase)
                    ? true : false;

                    users = isAsc ? users.OrderBy(x => x.firstName) : users.OrderByDescending(x => x.firstName);
                }
            }


            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;
            users = users.Skip(skipResults ?? 0).Take(pageSize ?? 100);

            return await users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(string id)
        {
            return await dbcontext.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> UpdateAsync(User user)
        {
            //var existingUser = await dbcontext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            var existingUser = await dbcontext.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.Id == user.Id);

            if (existingUser == null) {
                return null;
            }

            dbcontext.Entry(existingUser).CurrentValues.SetValues(user);

            //foreach (var permission in user.UserPermissions)
            //{
            //    permission.userId = user.Id.ToString();
            //    existingUser.UserPermissions.Add(permission);
            //}

            existingUser.UserPermissions = user.UserPermissions;

            await dbcontext.SaveChangesAsync();

            return existingUser;
          
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var existingUser = await dbcontext.Users.Include(u => u.UserPermissions).FirstOrDefaultAsync(u => u.Id == id);
            if (existingUser == null)
            {
                return null;
            }

            if (existingUser.UserPermissions != null && existingUser.UserPermissions.Any())
            {
                dbcontext.UserPermissions.RemoveRange(existingUser.UserPermissions);
            }

            dbcontext.Users.Remove(existingUser);
            await dbcontext.SaveChangesAsync();
            return existingUser;
            
        }
    }
}
