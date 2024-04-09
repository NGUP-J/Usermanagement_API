using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Usermanagement_API.Models.Domain
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string? phone { get; set; }
        public string roleId { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public Role Role { get; set; }
        public ICollection<UserPermission>? UserPermissions { get; set; }

    }
}
