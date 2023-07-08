using Microsoft.EntityFrameworkCore;
using SeaBirdProject.ApplicationContext;
using SeaBirdProject.Entities;
using SeaBirdProject.Repositories.Interfaces;

namespace SeaBirdProject.Repositories.Implementations
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Admin> CreateAsync(Admin user)
        {
            await _context.Admins.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(Admin user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Admin>> GetAllAsync()
        {
            return await _context.Admins
               .Include(s => s.User)
               .ThenInclude(s => s.Branch)
               .Where(s => s.User.Role.Equals("Admin") && s.User.IsActive.Equals(true) && s.User.IsRegistered.Equals(true))
               .ToListAsync();
        }

        public async Task<IEnumerable<Admin>> GetAllNonActiveAsync()
        {
            return await _context.Admins
              .Include(s => s.User)
              .ThenInclude(s => s.Branch)
              .Where(s => s.User.Role.Equals("Admin") && s.User.IsActive.Equals(false) && s.User.IsRegistered.Equals(true))
              .ToListAsync();
        }

        public Admin GetByEmail(string userEmail)
        {
            return _context.Admins.Include(s => s.User).ThenInclude(s => s.Branch).
            SingleOrDefault(s => s.User.Email.Equals(userEmail));
        }

        public Admin GetById(string userId)
        {
            return _context.Admins.Include(s => s.User).ThenInclude(s => s.Branch).
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

        public async Task<IEnumerable<Admin>> SearchAdminByEmailOrUsername(string searchInput)
        {
            return await _context.Admins.
              Include(s => s.User).
              ThenInclude(s => s.Branch).
              Where(s => s.User.Email.ToLower().Equals(searchInput.ToLower()) || s.User.UserName.ToLower().Equals(searchInput.ToLower())).
              ToListAsync();
        }

        public Admin Update(Admin user)
        {
            _context.SaveChanges();
            return user;
        }
    }
}
