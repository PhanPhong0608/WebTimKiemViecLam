using DACS_WebTimKiemViecLam.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public class EFCompanyRepository : ICompanyRepository
    {
        private readonly JobDbContext _context;
        private readonly DbSet<Company> _dbSet;

        public EFCompanyRepository(JobDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Company>();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _context.Companies
                         .Include(c => c.Field)
                         .ToListAsync();
        }
        public async Task<Company> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(Company entity) => await _dbSet.AddAsync(entity);
        public void Update(Company entity) => _dbSet.Update(entity);
        public void Delete(Company entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
