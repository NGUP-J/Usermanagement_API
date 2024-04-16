using Usermanagement_API.Models.Domain;

namespace Usermanagement_API.Models.DTO
{
    public class GetUserDto
    {

        public string UserId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public Role Role { get; set; }
        public string username { get; set; }
        public List<Permission>? permissions { get; set; }
        public string CreatedDate { get; set; }
    }
}
