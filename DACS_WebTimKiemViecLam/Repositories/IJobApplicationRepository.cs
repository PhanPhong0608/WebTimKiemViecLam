using DACS_WebTimKiemViecLam.Models;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public interface IJobApplicationRepository
    {
        Task<IEnumerable<JobApplication>> GetAllAsync();
        Task<JobApplication> GetByIdAsync(int id);
        Task AddAsync(JobApplication entity);
        void Update(JobApplication entity);
        void Delete(JobApplication entity);
        Task SaveAsync();
    }
}
