using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkbaseApi.Data;
using WorkbaseApi.Enums;
using WorkbaseApi.Models;

namespace WorkbaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly WorkbaseDbContext _workbaseDbContext;
        private readonly IConfiguration _configuration;
        public AuthController(WorkbaseDbContext workbaseDbContext, IConfiguration configuration)
        {
            _workbaseDbContext = workbaseDbContext;
            _configuration = configuration;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                var user = _workbaseDbContext.Users.FirstOrDefault(u => u.Email == request.Email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                {
                    return Unauthorized(ApiResponse<string>.Error("Invalid credentials"));
                }
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, ((RoleType)user.RoleId).ToString()),
                    new Claim("TenantId", user.TenantId.ToString())
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    claims: claims,
                    expires: DateTime.Now.AddHours(12),
                    signingCredentials: creds
                    );
                var response = new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Email = user.Email,
                    Role = ((RoleType)user.RoleId).ToString(),
                    TenantId = user.TenantId
                };
                return Ok(ApiResponse<LoginResponse>.Success(response, "Login successful"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error($"{ex.Message}"));
            }
        }

        //Temporary
        [HttpGet("all-superadmins")]
        public IActionResult GetAllSuperAdmins()
        {
            try
            {
                // Query to get all users with SuperAdmin role
                var superAdmins = _workbaseDbContext.Users
                    .Where(u => u.RoleId == (int)RoleType.SuperAdmin)
                    .Select(u => new UserResponse
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        RoleType = RoleType.SuperAdmin.ToString(),
                        TenantId = u.TenantId
                    })
                    .ToList();

                if (superAdmins.Count == 0)
                {
                    return NotFound(ApiResponse<UserResponse>.Error("No Super Admins found."));
                }

                return Ok(ApiResponse<List<UserResponse>>.Success(superAdmins, "Super Admins retrieved successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<UserResponse>.Error($"Error: {ex.Message}"));
            }
        }


    }
}
