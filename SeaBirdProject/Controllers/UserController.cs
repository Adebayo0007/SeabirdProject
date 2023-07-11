using Microsoft.AspNetCore.Mvc;
using SeaBirdProject.ApplicationAuthenticationFolder;
using SeaBirdProject.Dtos.UserDto;
using SeaBirdProject.Services.Interfaces;
using System.Security.Claims;

namespace SeaBirdProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTAuthentication _authentication;
        public UserController(IUserService userService, IJWTAuthentication authentication)
        {
            _authentication = authentication;
            _userService = userService;
        }

        [HttpPost("LogIn")]
        public async Task<IActionResult> LogIn(LoginRequestModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                string response1 = "Invalid input,check your input";
                return BadRequest(new{ message = response1});
            }
            if (string.IsNullOrWhiteSpace(loginModel.Email) || string.IsNullOrWhiteSpace(loginModel.Password))
            {
                return BadRequest();
            }
            var user = await _userService.Login(loginModel);
            if (!user.IsSuccess)
            {
                return BadRequest(user);
            }

            var token = _authentication.GenerateToken(user);
            var response = new LoginResponseModel<UserDto>
            {
                Data = user.Data,
                Message = user.Message,
                Token = token,
                IsSuccess = true
            };
            return Ok(response);

        }

        //[Authorize(Roles = "SuperAdmin")]
        [HttpGet("ApplicationUsers")]
        public async Task<IActionResult> ApplicationUsers()
        {
            var users = await _userService.GetAllAsync();
            if (users.IsSuccess == false)
            {
                return BadRequest(users);
            }
            return Ok(users);

        }

        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                try
                {
                    userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                }
                catch (Exception ex)
                {
                    throw new Exception($"You need to login, the exception message says: {ex.Message}");
                }
            }
            var user = await _userService.GetByIdAsync(userId);
            if (user.IsSuccess == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpGet("GetUserByEmail/{userEmail}")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string userEmail)
        {
            if (string.IsNullOrWhiteSpace(userEmail))
            {
                try
                {
                    userEmail = User.FindFirst(ClaimTypes.Email).Value;
                }
                catch (Exception ex)
                {
                    throw new Exception($"You need to login, the exception message says: {ex.Message}");
                }
            }
            var user = await _userService.GetByEmailAsync(userEmail);
            if (user.IsSuccess == false)
            {
                return BadRequest(user);
            }
            return Ok(user);
        }

        [HttpPatch("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest();
            }
            await _userService.DeleteAsync(userId);
            string response = "user deleted successfully";
            return Ok(new{ message = response });
        }

        [HttpGet("SearchUser/{searchInput}")]
        public async Task<IActionResult> SearchUser([FromRoute] string searchInput)
        {
            if (string.IsNullOrWhiteSpace(searchInput))
            {
                return BadRequest();
            }

            var users = await _userService.SearchUserByEmailOrUserName(searchInput);
            if (users.IsSuccess == false)
            {
                return BadRequest(users);
            }
            return Ok(users);
        }

        [HttpGet("PendingRegistration")]
        public async Task<IActionResult> PendingRegistration()
        {
            var pendingRequests = await _userService.PendingRegistration();
            if (pendingRequests.IsSuccess.Equals(false))
            {
                return BadRequest(pendingRequests);
            }
            return Ok(pendingRequests);
        }

        [HttpPatch("VerifyUser/{userEmail}")]
        public IActionResult VerifyUser([FromRoute] string userEmail)
        {
            if (!(string.IsNullOrWhiteSpace(userEmail)))
            {
                var user = _userService.VerifyUser(userEmail);
                return Ok(user);
            }
            return BadRequest();
        }

        [HttpPost("UpdatePassword")]
        public async Task<IActionResult> UpdatePassword(ForgetPasswordRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = _userService.UpdatePassword(model);
            if(response.IsSuccess.Equals(false))
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
