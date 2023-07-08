using SeaBirdProject.Entities;

namespace SeaBirdProject.Repositories.Interfaces
{
    public interface IStaffRepository : IBaseRepository<Staff>
    {
        Task<IEnumerable<Staff>> SearchStaffByEmailOrUsername(string searchInput);
    }
}
