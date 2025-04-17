using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Models
{
    public class JobDbContext : DbContext
    {
        public JobDbContext(DbContextOptions<JobDbContext> options)
            : base(options) { }

        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Company> Companies { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
