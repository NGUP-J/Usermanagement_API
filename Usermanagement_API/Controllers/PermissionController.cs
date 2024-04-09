using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionRepository permissionRepository;
        public PermissionController(IPermissionRepository permissionRepository)
        {

            this.permissionRepository = permissionRepository;

        }

        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await permissionRepository.GetPermissionsAsync();
            var data = new List<Permission>();
            foreach (var permission in permissions)
            {
               data.Add(new Permission
               {
                   permissionId = permission.permissionId,
                   permissionName = permission.permissionName,
           
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
