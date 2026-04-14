using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;

namespace RepositoryLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<MeasurementRecord> Measurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MeasurementRecord>().ToTable("measurements");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
        }
    }
}