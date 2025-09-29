namespace WorkbaseApi.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public int TenantId { get; set; }
    }
}
