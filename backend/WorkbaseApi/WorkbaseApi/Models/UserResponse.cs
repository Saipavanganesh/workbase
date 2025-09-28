using WorkbaseApi.Enums;

namespace WorkbaseApi.Models
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleType { get; set; } = null!;
        public int TenantId { get; set; }
    }
}
