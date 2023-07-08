using SeaBirdProject.CustomValidation;
using SeaBirdProject.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SeaBirdProject.Dtos.AdminDto
{
    public class CreateAdminRequestModel
    {
        [Required]
        [MaxLength(10)]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Profile Picture")]
        public string? ProfilePicture { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(14)]
        [MinLength(10)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        [EmailValidation]
        public string Email { get; set; }
        [Required]
        [EmailValidation]
        [Compare("Email")]
        [DisplayName("Confirm Email")]
        public string ConfirmEmail { get; set; }
        [Required]
        [PasswordValidation]
        public string Password { get; set; }
        [Required]
        [PasswordValidation]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [Required]
        [DisplayName("Branch Type")]
        public string BranchType { get; set; }
        [Required]
        [DisplayName("Branch Address")]
        public string BranchAddress { get; set; }  
    }
}
