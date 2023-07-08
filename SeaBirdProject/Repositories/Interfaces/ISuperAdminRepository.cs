using Mysqlx.Notice;
using SeaBirdProject.Entities;

namespace SeaBirdProject.Repositories.Interfaces
{
    public interface ISuperAdminRepository : IBaseRepository<SuperAdmin>
    {
        Task<IEnumerable<SuperAdmin>> SearchSuperAdminByEmailOrUsername(string searchInput);
    }
}
