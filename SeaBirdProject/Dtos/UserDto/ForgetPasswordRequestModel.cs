using SeaBirdProject.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace SeaBirdProject.Dtos.UserDto
{
    public class ForgetPasswordRequestModel
    {
        [Required]
        [EmailAddress]
        [EmailValidation]
        public string Email { get; set; }
        [Required]
        [PasswordValidation]
        public string NewPassword { get; set; }
    }
}
