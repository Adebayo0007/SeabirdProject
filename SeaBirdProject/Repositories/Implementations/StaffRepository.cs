using Microsoft.EntityFrameworkCore;
using SeaBirdProject.ApplicationContext;
using SeaBirdProject.Entities;
using SeaBirdProject.Repositories.Interfaces;

namespace SeaBirdProject.Repositories.Implementations
{
    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _context;
        public StaffRepository(ApplicationDbContext context)
        {
            try
            {
             _context = context;
            }
            catch (Exception ex)
            {
                throw new Exception($"The exception message says: {ex.Message}");
            }
        }
        public async Task<Staff> CreateAsync(Staff user)
        {
            await _context.Staffs.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(Staff user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllAsync()
        {
            return await _context.Staffs
              .Include(s => s.User)
              .ThenInclude(s => s.Branch)
              .Where(s => s.User.Role.Equals("Staff") && s.User.IsActive.Equals(true) && s.User.IsRegistered.Equals(true))
              .ToListAsync();
        }

        public async Task<IEnumerable<Staff>> GetAllNonActiveAsync()
        {
            return await _context.Staffs
               .Include(s => s.User)
               .ThenInclude(s => s.Branch)
               .Where(s => s.User.Role.Equals("Staff") && s.User.IsActive.Equals(false) && s.User.IsRegistered.Equals(true))
               .ToListAsync();
        }

        public Staff GetByEmail(string userEmail)
        {
            return _context.Staffs.Include(s => s.User).ThenInclude(s => s.Branch).
            SingleOrDefault(s => s.User.Email.Equals(userEmail));
        }

        public Staff GetById(string userId)
        {
            return _context.Staffs.Include(s => s.User).ThenInclude(s => s.Branch).
           SingleOrDefault(s => s.UserId.Equals(userId));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Staff>> SearchStaffByEmailOrUsername(string searchInput)
        {
            return await _context.Staffs.
            Include(s => s.User).
            ThenInclude(s => s.Branch).
            Where(s => s.User.Email.ToLower().Equals(searchInput.ToLower()) || s.User.UserName.ToLower().Equals(searchInput.ToLower())).
            ToListAsync();
        }

        public Staff Update(Staff user)
        {
            _context.SaveChanges();
            return user;
        }
    }
}
