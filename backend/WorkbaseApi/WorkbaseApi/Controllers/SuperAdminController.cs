using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkbaseApi.Data;
using WorkbaseApi.Entities;
using WorkbaseApi.Enums;
using WorkbaseApi.Models;

namespace WorkbaseApi.Controllers
{
    [ApiController]
    [Route("api/v1/superadmin")]
    public class SuperAdminController : ControllerBase
    {
        private readonly WorkbaseDbContext _workbaseDbContext;
        public SuperAdminController(WorkbaseDbContext workbaseDbContext)
        {
            _workbaseDbContext = workbaseDbContext;
        }

        [HttpPost("create")]
        public IActionResult CreateSuperAdmin([FromBody] CreateSuperAdminRequest request)
        {
            try
            {
                var exists = _workbaseDbContext.Users.Any(u => u.RoleId == (int)RoleType.SuperAdmin && u.Email == request.Email);
                if (exists)
                {
                    return BadRequest(ApiResponse<UserResponse>.Error($"Super Admin with this email already exists: {request.Email}"));
                }
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var superAdmin = new User
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    RoleId = (int)RoleType.SuperAdmin,
                    TenantId = 0
                };
                _workbaseDbContext.Users.Add(superAdmin);
                _workbaseDbContext.SaveChanges();

                var response = new UserResponse
                {
                    Id = superAdmin.Id,
                    FirstName = superAdmin.FirstName,
                    LastName = superAdmin.LastName,
                    Email = superAdmin.Email,
                    RoleType = RoleType.SuperAdmin.ToString(),
                    TenantId = superAdmin.TenantId
                };

                return Ok(ApiResponse<UserResponse>.Success(response, "Super Admin created successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error($"{ex.Message}"));
            }
        }
    }
}


//  "email": "saipavanganesh@workbase.com",
//"password": "Superadmin@123"
