using Microsoft.EntityFrameworkCore;
using SeaBirdProject.Entities;

namespace SeaBirdProject.ApplicationContext;
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Branch> Branches{ get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<Sale> Sales { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Branch>().HasData(
            new Branch
            {
                Id = "cc7578e3-52a9-49e9-9788-2da54df19f38",
                BranchName = "All Branches",
                BranchType = "All Type",
                BranchAddress = "All Branches Address",
            });

        modelBuilder.Entity<User>().HasData(
          new User
          {
              Id = "37846734-732e-4149-8cec-6f43d1eb3f60",
              BranchId = "cc7578e3-52a9-49e9-9788-2da54df19f38",
              Role = "SuperAdmin",
              IsActive = true,
              Password = BCrypt.Net.BCrypt.HashPassword("Admin0001"),
              UserName = "Modrator",
              Name = "Adebayo Addullah",
              PhoneNumber = "08087054632",
              Gender = "Male",
              Email = "tijaniadebayoabdllahi@gmail.com",
              DateCreated = DateTime.Now,
              IsRegistered = true,
              Address = "10,Abayomi street,Ipaja,lagos"

          }
      );


            modelBuilder.Entity<SuperAdmin>().HasData(
           new SuperAdmin
           {
               Id = "37846734-732e-4149-8cec-6f43d1eb3f53",
               UserId = "37846734-732e-4149-8cec-6f43d1eb3f60"
           }
       );
    }
}

