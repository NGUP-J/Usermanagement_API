using System.ComponentModel.DataAnnotations;
using Usermanagement_API.Models.Domain;

namespace Usermanagement_API.Models.DTO
{
    public class CreateUserRequestDto
    {

        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        public string? phone { get; set; }
        [Required]
        public string roleId { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public List<userPermissionDto>? Permissions { get; set; }
    }

    public class userPermissionDto
    {
        public string? permissionId { get; set; }
        public bool isReadable { get; set; }
        public bool isWritable { get; set; }
        public bool isDeletable { get; set; }

    }
}
