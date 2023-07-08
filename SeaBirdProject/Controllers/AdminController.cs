using Microsoft.AspNetCore.Mvc;
using SeaBirdProject.Dtos.AdminDto;
using SeaBirdProject.Services.Interfaces;

namespace SeaBirdProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserService _userService;
        public AdminController(IAdminService adminService, IUserService userService)
        {
            _adminService = adminService;
            _userService = userService;
        }

        [HttpPost("CreateAdmin")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin([FromForm] CreateAdminRequestModel adminModel)
        {

            if (!ModelState.IsValid)
            {
                string response = "Invalid input,check your input very well";
                return BadRequest(new { mesage = response });
            }
          

            var buyerExist = await _userService.ExistByEmailAsync(adminModel.Email);
            if (!(buyerExist))
            {

                //handling the files in coming from the request

               /* var files = HttpContext.Request.Form;
                if (files != null && files.Count > 0)
                {
                    string imageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                    if (!Directory.Exists(imageDirectory)) Directory.CreateDirectory(imageDirectory);
                    foreach (var file in files.Files)
                    {
                        FileInfo info = new FileInfo(file.FileName);
                        var extension = info.Extension;
                        string[] extensions = new string[] { ".png", ".jpeg", ".jpg", ".gif", ".tif" };
                        bool check = false;
                        foreach (var ext in extensions)
                        {
                            if (extension.Equals(ext)) check = true;
                        }
                        if (check == false) return BadRequest(new { mesage = "The type of your profile picture is not accepted" });
                        if (file.Length > 20480) return BadRequest(new { mesage = "Accepted profile picture must not be more than 20KB" });
                        string image = Guid.NewGuid().ToString() + info.Extension;
                        string path = Path.Combine(imageDirectory, image);
                        using (var filestream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(filestream);
                        }
                        buyerModel.ProfilePicture = (image);
                    }
                }*/
                var admin = await _adminService.CreateAsync(adminModel);

                if (admin.IsSuccess.Equals(false))
                {
                    return BadRequest(admin);
                }

                return Ok(admin);
            }
            string userExist = "user already exist ⚠";
            return BadRequest(new { mesage = userExist });

        }
    }
}
