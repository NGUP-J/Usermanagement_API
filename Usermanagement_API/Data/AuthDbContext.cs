using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Usermanagement_API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "1b4d7046-076a-4a50-9969-dc8ec5a0849c";
            var editorRoleId = "48cb64da-4793-4dfe-8158-1653fe843113";

            // Create editor and reader writer roles
            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "READER",
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id = editorRoleId,
                    Name = "Editor",
                    NormalizedName = "EDITOR",
                    ConcurrencyStamp = editorRoleId
                }
            };

            // Seed roles
            builder.Entity<IdentityRole>().HasData(roles);

            // Create admin user
            var adminUserId = "5715c437-a5e2-4936-8579-b82d8a6662b7";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@example.com",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com".ToUpper(),
                NormalizedUserName = "admin@example.com".ToUpper(),
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);
            // Give Roles To Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleId
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = editorRoleId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
