using SeaBirdProject.CustomValidation;

namespace SeaBirdProject.Dtos.SuperAdminDto
{
    public class UpdateSuperAdminRequestModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
   /*     [EmailValidation]
        public string Email { get; set; }
        [PasswordValidation]
        public string Password { get; set; }*/
        public string BranchName { get; set; }   
        public string BranchType { get; set; }   
        public string BranchAddress { get; set; }  
    }
}
