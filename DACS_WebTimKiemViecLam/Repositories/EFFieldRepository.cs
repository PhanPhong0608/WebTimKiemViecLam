using DACS_WebTimKiemViecLam.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public class EFFieldRepository : IFieldRepository
    {
        private readonly JobDbContext _context;
        private readonly DbSet<Field> _dbSet;

        public EFFieldRepository(JobDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Field>();
        }

        public async Task<IEnumerable<Field>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<Field> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(Field entity) => await _dbSet.AddAsync(entity);
        public void Update(Field entity) => _dbSet.Update(entity);
        public void Delete(Field entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
