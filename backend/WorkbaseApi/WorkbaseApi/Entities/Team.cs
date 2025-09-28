namespace WorkbaseApi.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
