using SeaBirdProject.Entities;
using SeaBirdProject.Repositories.Interfaces;

namespace SeaBirdProject.Repositories.Implementations
{
    public class StaffRepository : IStaffRepository
    {
        public Task<Staff> CreateAsync(Staff user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Staff user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Staff>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Staff>> GetAllNonActiveAsync()
        {
            throw new NotImplementedException();
        }

        public Staff GetByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Staff GetById(string userId)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Staff>> SearchStaffByEmailOrUsername(string searchInput)
        {
            throw new NotImplementedException();
        }

        public Staff Update(Staff user)
        {
            throw new NotImplementedException();
        }
    }
}
