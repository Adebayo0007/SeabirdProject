using Microsoft.AspNetCore.Mvc;
using SeaBirdProject.Dtos.SuperAdminDto;
using SeaBirdProject.Services.Interfaces;
using System.Security.Claims;

namespace SeaBirdProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        private readonly ISuperAdminService _superAdminService;
        public SuperAdminController(ISuperAdminService superAdminServic)
        {
            _superAdminService = superAdminServic;
        }

        [HttpGet(" GetSuperAdminById/{userId}")]
        public async Task<IActionResult> GetSuperAdminById([FromRoute] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId)) userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var superadminResonse = await _superAdminService.GetByIdAsync(userId);
            if (superadminResonse.IsSuccess == false)
            {
                return BadRequest(superadminResonse);
            }
            return Ok(superadminResonse);
        }

        [HttpGet("GetSuperAdminByEmail/{superAdminEmail}")]
        public async Task<IActionResult> GetSuperAdminByEmail([FromRoute] string superAdminEmail)
        {
            if (string.IsNullOrWhiteSpace(superAdminEmail)) superAdminEmail = User.FindFirst(ClaimTypes.Email).Value;
            var superAdminResponse = await _superAdminService.GetByEmailAsync(superAdminEmail);
            if (superAdminResponse.IsSuccess == false)
            {
                return BadRequest(superAdminResponse);
            }
            return Ok(superAdminResponse);
        }
        [HttpGet("SuperAdmins")]
        public async Task<IActionResult> SuperAdmins()
        {
            var superAdminsResponse = await _superAdminService.GetAllAsync();
            if (superAdminsResponse.IsSuccess == false)
            {
                return BadRequest(superAdminsResponse);
            }
            return Ok(superAdminsResponse);

        }

        [HttpPatch("DeleteSuperAdmin/{userId}")]
        public async Task<IActionResult> DeleteSuperAdmin([FromRoute] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest();
            }
            await _superAdminService.DeleteAsync(userId);
            string response = "user deleted successfully";
            return Ok(new { message = response });
        }

        [HttpGet("SearchSuperAdmin/{searchInput}")]
        public async Task<IActionResult> SearchSuperAdmin([FromRoute] string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                return BadRequest();
            }

            var superAdmins = await _superAdminService.SearchSuperAdminByEmailOrUserName(searchInput);
            if (superAdmins.IsSuccess == false)
            {
                return BadRequest(superAdmins);
            }
            return Ok(superAdmins);
        }

        [HttpPut("UpdateSuperAdmin/{id}")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateSuperAdmin(UpdateSuperAdminRequestModel updateRequestModel, string id)
        {
            if (!ModelState.IsValid)
            {
                string response = "Invalid input,check your input very well";
                return BadRequest(new { mesage = response });
            }

            if (string.IsNullOrWhiteSpace(id)) id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var SuperAdmin = await _superAdminService.UpdateAsync(updateRequestModel, id);
            if (SuperAdmin.IsSuccess == false)
            {
                return BadRequest(SuperAdmin);
            }
            return Ok(SuperAdmin);
        }
    }
}
