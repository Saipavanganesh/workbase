namespace WorkbaseApi.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
