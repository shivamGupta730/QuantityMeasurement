using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using ModelLayer.Enum;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<MeasurementRecord> Measurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<MeasurementRecord>(entity =>
    {
        entity.ToTable("measurements");

        entity.HasKey(e => e.Id);
        entity.Property(e => e.FromUnit).HasMaxLength(50);
        entity.Property(e => e.ToUnit).HasMaxLength(50);
        entity.Property(e => e.MeasurementType).HasConversion<string>();
        entity.Property(e => e.OperationType).HasConversion<string>();
        entity.Property(e => e.ErrorMessage).HasMaxLength(500);
    });

    modelBuilder.Entity<User>(entity =>
    {
        entity.ToTable("users");

        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Email).IsUnique();
        entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
        entity.Property(e => e.PasswordHash).IsRequired();
    });

    modelBuilder.Entity<ApplicationUser>(entity =>
    {
        entity.ToTable("applicationusers");

        entity.HasKey(e => e.Id);
        entity.HasIndex(e => e.Email).IsUnique();
        entity.Property(e => e.Email).HasMaxLength(256).IsRequired();
    });
}

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
