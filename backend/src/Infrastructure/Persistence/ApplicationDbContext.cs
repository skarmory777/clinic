using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Configurations;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.UseCollation("utf8mb4_general_ci");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsConfiguration).Assembly);
        }
    }
}