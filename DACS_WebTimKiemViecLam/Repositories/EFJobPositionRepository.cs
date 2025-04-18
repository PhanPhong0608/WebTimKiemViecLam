using DACS_WebTimKiemViecLam.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public class EFJobPositionRepository : IJobPositionRepository
    {
        private readonly JobDbContext _context;
        private readonly DbSet<JobPosition> _dbSet;

        public EFJobPositionRepository(JobDbContext context)
        {
            _context = context;
            _dbSet = context.Set<JobPosition>();
        }

        public async Task<IEnumerable<JobPosition>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<JobPosition> GetByIdAsync(int id)
        {
            return await _context.JobPositions
                .Include(j => j.Company)
                .Include(j => j.Field)
                .FirstOrDefaultAsync(j => j.JobID == id);
        }
        public async Task AddAsync(JobPosition entity) => await _dbSet.AddAsync(entity);
        public void Update(JobPosition entity) => _dbSet.Update(entity);
        public void Delete(JobPosition entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
