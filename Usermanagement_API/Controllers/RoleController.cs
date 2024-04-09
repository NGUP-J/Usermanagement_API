using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await roleRepository.GetRolesAsync();
            var data = new List<Role>();
            foreach (var role in roles)
            {
                data.Add(new Role
                {
                    roleId = role.roleId,
                    roleName = role.roleName,
            
                });
            }
            var response = new
            {
                Status = new
                {
                    Code = 200,
                    Message = "Success"
                },
                Data = data
            };

            return Ok(response);
        }   
    }
}
