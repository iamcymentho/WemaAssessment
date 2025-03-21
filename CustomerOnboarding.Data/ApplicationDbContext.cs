using CustomerOnboarding.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerOnboarding.Data
{
    public class CustomerOnboardingDbContext : DbContext
    {
        public CustomerOnboardingDbContext(DbContextOptions<CustomerOnboardingDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique(); // Ensure phone number is unique
        }
    }
}


