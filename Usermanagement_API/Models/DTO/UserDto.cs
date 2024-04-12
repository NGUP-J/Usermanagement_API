using System.ComponentModel.DataAnnotations;
using Usermanagement_API.Models.Domain;

namespace Usermanagement_API.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; } = "";
        public Role Role { get; set; }
        public string username { get; set; }
        public List<PermissionDto>? permissions { get; set; }
    }
    public class PermissionDto
    {
        public string? permissionId { get; set; }
        public string? permissionName { get; set; }
        public bool isReadable { get; set; }
        public bool isWritable { get; set; }
        public bool isDeletable { get; set; }

    }
}
