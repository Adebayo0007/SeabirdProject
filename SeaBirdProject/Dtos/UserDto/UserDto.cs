using SeaBirdProject.Entities;

namespace SeaBirdProject.Dtos.UserDto
{
    public class UserDto
    {
        public string Id { get; set; } 
        public string? BranchId { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public bool IsRegistered { get; set; }
        public string BranchName { get; set; }   
        public string BranchType { get; set; }  
        public string BranchAddress { get; set; } 
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
