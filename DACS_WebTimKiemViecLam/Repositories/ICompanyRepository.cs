using DACS_WebTimKiemViecLam.Models;

namespace DACS_WebTimKiemViecLam.Repositories
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task AddAsync(Company entity);
        void Update(Company entity);
        void Delete(Company entity);
        Task SaveAsync();
    }
}
