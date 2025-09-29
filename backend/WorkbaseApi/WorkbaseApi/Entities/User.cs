namespace WorkbaseApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        //ForeignKeys
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int? ManagerId { get; set; }
        public User? Manager { get; set; }
        public ICollection<User> Subordinates { get; set; } = new HashSet<User>();
        public bool IsPrimaryAdmin { get; set; } = false;
    }
}
