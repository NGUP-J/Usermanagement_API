using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Numerics;
using Usermanagement_API.Data;
using Usermanagement_API.Models.Domain;
using Usermanagement_API.Models.DTO;
using Usermanagement_API.Repositories.Implementation;
using Usermanagement_API.Repositories.Interface;

namespace Usermanagement_API.Controllers
{

    // https://localhost:5001/api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
   
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IPermissionRepository permissionRepository;

        public UserController(IUserRepository userRepository,
            IRoleRepository roleRepository,
            IPermissionRepository permissionRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto request)
        {
            // Map DTO to Domain Model
            var user = new User
            {
                Id = request.Id,
                firstName = request.firstName,
                lastName = request.lastName,
                email = request.email,
                phone = request.phone,
                roleId = request.roleId,
                username = request.username,
                password = request.password,
                UserPermissions = new List<UserPermission>(),
            };
            foreach (var permissionDto in request.Permissions)
            {
                if (permissionDto != null)
                {
                    user.UserPermissions.Add(new UserPermission
                    {
                        User = user,
                        permissionId = permissionDto.permissionId,
                        isReadable = permissionDto.isReadable,
                        isWritable = permissionDto.isWritable,
                        isDeletable = permissionDto.isDeletable,
                    });
                }
            }

            //user.UserPermissions = request.Permissions.Select(p => new UserPermission
            //{
            //    userId = user.Id.ToString(),
            //    permissionId = p.permissionId,
            //    isReadable = p.isReadable,
            //    isWritable = p.isWritable,
            //    isDeletable = p.isDeletable,
            //}).ToList();


            // Add to database
            user = await userRepository.CreateAsync(user);

            // Domain model to DTO
            var data = new UserDto
            {
                Id = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phone = user.phone,
                Role = new Role
                {
                    roleId = user.roleId,
                    roleName = roleRepository.GetRoleByIdAsync(user.roleId).Result.roleName,
                },
                username = user.username,
                permissions = user.UserPermissions.Select(p => new PermissionDto
                {
                    permissionId = p.permissionId,
                    permissionName = permissionRepository.GetPermissionByIdAsync(p.permissionId).Result.permissionName,
                    isReadable = p.isReadable,
                    isWritable = p.isWritable,
                    isDeletable = p.isDeletable
                }).ToList(),
            };

            var response = new
            {
                Status = new
                {
                    code = StatusCodes.Status200OK.ToString(),
                    description = "User created successfully",
                },
                data
            };

            return Ok(response);
        }

        // GET: api/user
        [HttpGet]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string? search,
            [FromQuery] string? orderBy,
            [FromQuery] string? orderDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var users = await userRepository
                .GetUsersAsync(search, orderBy, orderDirection, pageNumber, pageSize);
            var dataSource = new List<GetUserDto>();
            foreach (var user in users)
            {
                dataSource.Add(new GetUserDto
                {
                    UserId = user.Id,
                    firstName = user.firstName,
                    lastName = user.lastName,
                    email = user.email,
                    Role = new Role
                    {
                        roleId = user.roleId,
                        roleName = roleRepository.GetRoleByIdAsync(user.roleId).Result.roleName,
                    },
                    username = user.username,
                    permissions = user.UserPermissions.Select(p => new Permission
                    {
                        permissionId = p.permissionId,
                        permissionName = permissionRepository.GetPermissionByIdAsync(p.permissionId).Result.permissionName,
                    }).ToList(),
                    CreatedDate = user.CreatedDate,
                });
            }
            var response = new
            {
                dataSource,
                page = pageNumber ?? 1,
                pageSize = pageSize ?? 10,
                totalCount = dataSource.Count,
            };
            return Ok(response);
        }

        // GET: api/user/{id}}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var data = new UserDto
            {
                Id = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phone = user.phone,
                Role = new Role
                {
                    roleId = user.roleId,
                    roleName = roleRepository.GetRoleByIdAsync(user.roleId).Result.roleName,
                },
                username = user.username,
                //permissions = user.UserPermissions.Select(p => new Permission
                //{
                //    permissionId = p.permissionId,
                //    permissionName = permissionRepository.GetPermissionByIdAsync(p.permissionId).Result.permissionName,
                //}).ToList(),
                permissions = user.UserPermissions.Select(p => new PermissionDto
                {
                    permissionId = p.permissionId,
                    permissionName = permissionRepository.GetPermissionByIdAsync(p.permissionId).Result.permissionName,
                    isReadable = p.isReadable,
                    isWritable = p.isWritable,
                    isDeletable = p.isDeletable
                }).ToList(),    

            };

            var response = new
            {
                status = new
                {
                    code = StatusCodes.Status200OK.ToString(),
                    description = "User fetched successfully",
                },
                data
            };
            return Ok(response);
        }

        // PUT: api/user/{id}
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] string id, [FromBody] UpdateUserRequestDto request)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.firstName = request.firstName;
            user.lastName = request.lastName;
            user.email = request.email;
            user.phone = request.phone;
            user.roleId = request.roleId;
            user.username = request.username;
            user.password = request.password;

           foreach (var permissionDto in request.Permissions)
            {
                if (permissionDto != null)
                {
                    var temp = user.UserPermissions.FirstOrDefault(p => p.permissionId == permissionDto.permissionId);
                    if (temp == null)
                    {
                        user.UserPermissions.Add(new UserPermission
                        {
                            userId = id,
                            permissionId = permissionDto.permissionId,
                            isReadable = permissionDto.isReadable,
                            isWritable = permissionDto.isWritable,
                            isDeletable = permissionDto.isDeletable,
                        });
                    }
                    else if (permissionDto.permissionId == temp.permissionId)
                    {
                        temp.isReadable = permissionDto.isReadable;
                        temp.isWritable = permissionDto.isWritable;
                        temp.isDeletable = permissionDto.isDeletable;
                    }
                }
            }

            var updateUser = await userRepository.UpdateAsync(user);

            if (updateUser == null)
            {
                return NotFound();
            }

            // Convert domain model to DTO
            var data = new UserDto
            {
                Id = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                email = user.email,
                phone = user.phone,
                Role = new Role
                {
                    roleId = user.roleId,
                    roleName = roleRepository.GetRoleByIdAsync(user.roleId).Result.roleName,
                },
                username = user.username,
                permissions = user.UserPermissions.Select(p => new PermissionDto
                {
                    permissionId = p.permissionId,
                    permissionName = permissionRepository.GetPermissionByIdAsync(p.permissionId).Result.permissionName,
                    isReadable = p.isReadable,
                    isWritable = p.isWritable,
                    isDeletable = p.isDeletable
                }).ToList(),

            };

            var response = new
            {
                status = new
                {
                    code = StatusCodes.Status200OK.ToString(),
                    description = "User updated successfully",
                },
                data
            };

            return Ok(response);
        }

        // DELETE: api/user/{id}
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var user = await userRepository.DeleteAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Convert domain model to DTO

            var response = new
            {
                status = new
                {
                    code = StatusCodes.Status200OK.ToString(),
                    description = "User deleted successfully",
                },
                data = new
                {
                    result = true,
                    message = "User deleted successfully"
                }
            };

            return Ok(response);
        }
    }

}
