using DACS_WebTimKiemViecLam.Models;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public interface IFieldRepository
    {
        Task<IEnumerable<Field>> GetAllAsync();
        Task<Field> GetByIdAsync(int id);
        Task AddAsync(Field entity);
        void Update(Field entity);
        void Delete(Field entity);
        Task SaveAsync();
    }
}
