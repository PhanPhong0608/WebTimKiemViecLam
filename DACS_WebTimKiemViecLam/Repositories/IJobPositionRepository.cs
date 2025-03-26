using DACS_WebTimKiemViecLam.Models;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public interface IJobPositionRepository
    {
        Task<IEnumerable<JobPosition>> GetAllAsync();
        Task<JobPosition> GetByIdAsync(int id);
        Task AddAsync(JobPosition entity);
        void Update(JobPosition entity);
        void Delete(JobPosition entity);
        Task SaveAsync();
    }
}
