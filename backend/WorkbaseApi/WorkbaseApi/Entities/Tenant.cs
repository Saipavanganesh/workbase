namespace WorkbaseApi.Entities
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? SubscriptionType { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Department> Departments { get; set; } = new List<Department>();

    }
}
