using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using dotnet_salary_master.Entities;
using dotnet_salary_master.enums;
using Microsoft.AspNetCore.Identity;

namespace dotnet_salary_master.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Employees = Set<Employee>();
            Users = Set<User>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Call base implementation first
            modelBuilder.Entity<User>()
                .Property(p => p.Role)
                .HasConversion<string>();
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<User> Users { get; set; }

    }

    public class ApplicationUser : IdentityUser
    {
        public UserRoles Role { get; set; } = UserRoles.Guest;
    }
}
