using HCM.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HCM.Infrastructure.Data
{
    public class HcmDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public HcmDbContext(DbContextOptions<HcmDbContext> options) : base(options) { }

        public DbSet<Person> People => Set<Person>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Person>().HasKey(p => p.Id);
            builder.Entity<Person>().Property(p => p.FirstName).IsRequired();
            builder.Entity<Person>().Property(p => p.LastName).IsRequired();
            builder.Entity<Person>().Property(p => p.Email).IsRequired();

            // Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "HR Admin", NormalizedName = "HR ADMIN" },
                new IdentityRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Id = "3", Name = "Employee", NormalizedName = "EMPLOYEE" }
            );
        }
    }
}
