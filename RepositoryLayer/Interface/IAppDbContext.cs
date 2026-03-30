using Microsoft.EntityFrameworkCore;
using ModelLayer.Entity;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAppDbContext
    {
        DbSet<MeasurementRecord> Measurements { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }

        Task<int> SaveChangesAsync();
        int SaveChanges();
    }
}

