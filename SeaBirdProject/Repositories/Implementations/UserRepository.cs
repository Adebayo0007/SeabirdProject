using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SeaBirdProject.ApplicationContext;
using SeaBirdProject.Entities;
using SeaBirdProject.Repositories.Interfaces;

namespace SeaBirdProject.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
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

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            return user;
        }

        public async Task Delete(User user)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistByEmailAsync(string userEmail)
        {
           return await _context.Users.AnyAsync( u => u.Email.Equals(userEmail));

        }

        public async Task<bool> ExistByPasswordAsync(string userPassword)
        {
            return await _context.Users.AnyAsync(u => u.Password.Equals(userPassword));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
           return  await _context.Users.
                Include(u => u.Branch).
                Include(u => u.SuperAdmin).
                Include(u => u.Admin).
                Include(u => u.Staff).
                Where(u => u.IsActive.Equals(true) && u.Role != "SuperAdmin" && u.IsRegistered.Equals(true)).
                ToListAsync();
        }

        public async Task<IEnumerable<User>> GetAllNonActiveAsync()
        {
            return await _context.Users.
               Include(u => u.Branch).
               Include(u => u.SuperAdmin).
               Include(u => u.Admin).
               Include(u => u.Staff).
               Where(u => u.IsActive.Equals(false) && u.Role != "SuperAdmin" && u.IsRegistered.Equals(true)).
               ToListAsync();
        }

        public User GetByEmail(string userEmail)
        {
           return _context.Users.Include(u => u.Branch).SingleOrDefault(u => u.Email.Equals(userEmail));
        }

        public User GetById(string userId)
        {
            return _context.Users.Include(u => u.Branch).SingleOrDefault(u => u.Id.Equals(userId));
        }

        public async Task<IEnumerable<User>> PendingRegistration()
        {
            return await _context.Users.Include(u => u.Branch).Where(u => u.IsRegistered.Equals(false)).ToListAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> SearchUserByEmailOrUsername(string searchInput)
        {
            return await _context.Users.
               Include(u => u.Branch).
               Include(u => u.SuperAdmin).
               Include(u => u.Admin).
               Include(u => u.Staff).
               Where(u => u.Email.ToLower().Equals(searchInput.ToLower()) || u.UserName.ToLower().Equals(searchInput.ToLower())).ToListAsync();
        }

        public User Update(User user)
        {
            _context.SaveChanges();
            return user;
        }
    }
}
