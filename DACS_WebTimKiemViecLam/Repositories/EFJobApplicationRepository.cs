using DACS_WebTimKiemViecLam.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public class EFJobApplicationRepository : IJobApplicationRepository
    {
        private readonly JobDbContext _context;
        private readonly DbSet<JobApplication> _dbSet;

        public EFJobApplicationRepository(JobDbContext context)
        {
            _context = context;
            _dbSet = context.Set<JobApplication>();
        }

        public async Task<IEnumerable<JobApplication>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<JobApplication> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(JobApplication entity) => await _dbSet.AddAsync(entity);
        public void Update(JobApplication entity) => _dbSet.Update(entity);
        public void Delete(JobApplication entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
