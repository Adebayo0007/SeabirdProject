using Microsoft.EntityFrameworkCore;
using SeaBirdProject.ApplicationContext;
using SeaBirdProject.Entities;
using SeaBirdProject.Repositories.Interfaces;

namespace SeaBirdProject.Repositories.Implementations
{
    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly ApplicationDbContext _context;
        public SuperAdminRepository(ApplicationDbContext context)
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
        public async Task<SuperAdmin> CreateAsync(SuperAdmin user)
        {
            await _context.SuperAdmins.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(SuperAdmin user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SuperAdmin>> GetAllAsync()
        {
            return await _context.SuperAdmins
                .Include(s => s.User)
                .ThenInclude(s => s.Branch)
                .Where(s => s.User.Role.Equals("SuperAdmin") && s.User.IsActive.Equals(true) && s.User.IsRegistered.Equals(true))
                .ToListAsync();
        }

        public async Task<IEnumerable<SuperAdmin>> GetAllNonActiveAsync()
        {
            return await _context.SuperAdmins
               .Include(s => s.User)
               .ThenInclude(s => s.Branch)
               .Where(s => s.User.Role.Equals("SuperAdmin") && s.User.IsActive.Equals(false) && s.User.IsRegistered.Equals(true))
               .ToListAsync();
        }

        public SuperAdmin GetByEmail(string userEmail)
        {
           return _context.SuperAdmins.Include(s => s.User).ThenInclude(s => s.Branch).
                SingleOrDefault(s => s.User.Email.Equals(userEmail));
        }

        public SuperAdmin GetById(string userId)
        {
            return _context.SuperAdmins.Include(s => s.User).ThenInclude(s => s.Branch).
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

        public async Task<IEnumerable<SuperAdmin>> SearchSuperAdminByEmailOrUsername(string searchInput)
        {
            return await _context.SuperAdmins.
               Include(s => s.User).
               ThenInclude(s => s.Branch).
               Where(s => s.User.Email.ToLower().Equals(searchInput.ToLower()) || s.User.UserName.ToLower().Equals(searchInput.ToLower())).
               ToListAsync();
        }

        public SuperAdmin Update(SuperAdmin user)
        {
            _context.SaveChanges();
            return user;
        }
    }
}
