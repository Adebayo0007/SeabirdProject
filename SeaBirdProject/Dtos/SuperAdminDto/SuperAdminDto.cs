namespace SeaBirdProject.Dtos.SuperAdminDto
{
    public class SuperAdminDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; }
        public string BranchName { get; set; }   
        public string BranchType { get; set; }   
        public string BranchAddress { get; set; }  
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
