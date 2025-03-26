using DACS_WebTimKiemViecLam.Models;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User entity);
        void Update(User entity);
        void Delete(User entity);
        Task SaveAsync();
        Task<User> GetByEmailAsync(string email);
    }
}
