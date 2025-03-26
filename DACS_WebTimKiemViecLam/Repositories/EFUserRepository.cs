using DACS_WebTimKiemViecLam.Models;
using Microsoft.EntityFrameworkCore;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly JobDbContext _context;
        private readonly DbSet<User> _dbSet;

        public EFUserRepository(JobDbContext context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<User> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public async Task AddAsync(User entity) => await _dbSet.AddAsync(entity);
        public void Update(User entity) => _dbSet.Update(entity);
        public void Delete(User entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();
        public async Task<User> GetByEmailAsync(string email) => await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
    }
}
