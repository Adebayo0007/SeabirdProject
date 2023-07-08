using SeaBirdProject.Entities;

namespace SeaBirdProject.Repositories.Interfaces
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        Task<IEnumerable<Admin>> SearchAdminByEmailOrUsername(string searchInput);
    }
}
