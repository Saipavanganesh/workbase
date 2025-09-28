using Microsoft.EntityFrameworkCore;
using WorkbaseApi.Entities;

namespace WorkbaseApi.Data
{
    public class WorkbaseDbContext : DbContext
    {
        public WorkbaseDbContext(DbContextOptions<WorkbaseDbContext> options) : base(options)
        {
            
        }
        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Self-reference for manager-subordinates
            modelBuilder.Entity<User>()
                .HasOne(u => u.Manager)
                .WithMany(u => u.Subordinates)
                .HasForeignKey(u => u.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed roles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "SuperAdmin" },
                new Role { Id = 2, Name = "TenantAdmin" },
                new Role { Id = 3, Name = "Manager" },
                new Role { Id = 4, Name = "Employee" }
            );
        }
    }
}
