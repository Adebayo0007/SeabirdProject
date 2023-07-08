using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.UserDto;

namespace SeaBirdProject.Services.Interfaces
{
    public interface IUserService
    {
        Task<BaseResponse<UserDto>> GetByIdAsync(string userId);
        Task<BaseResponse<UserDto>> GetByEmailAsync(string userEmail);
        Task<BaseResponse<UserDto>> Login(LoginRequestModel logInRequestModel);
        Task<BaseResponse<IEnumerable<UserDto>>> GetAllAsync();
        Task<BaseResponse<IEnumerable<UserDto>>> SearchUserByEmailOrUserName(string searchInput);
        Task<BaseResponse<IEnumerable<UserDto>>> PendingRegistration();
        BaseResponse<UserDto> UpdateAsync(UpdateUserRequestModel updateUserModel, string id);
        BaseResponse<UserDto> UpdatePassword(ForgetPasswordRequestModel model);
        Task DeleteAsync(string userId);
        BaseResponse<UserDto> VerifyUser(string userEmail);
        Task<bool> ExistByEmailAsync(string userEmail);
    }
}
