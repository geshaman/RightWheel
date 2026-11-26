using Microsoft.EntityFrameworkCore;
using RightWheel.Domain.Models;

namespace RightWheel.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Part> Parts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Category> Categories { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
