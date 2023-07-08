using SeaBirdProject.Dtos.SuperAdminDto;
using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.AdminDto;
using SeaBirdProject.Dtos.UserDto;

namespace SeaBirdProject.Services.Interfaces
{
    public interface IAdminService
    {
        Task<BaseResponse<AdminDto>> CreateAsync(CreateAdminRequestModel createAdminModel);
        Task<BaseResponse<AdminDto>> GetByIdAsync(string userId);
        Task<BaseResponse<AdminDto>> GetByEmailAsync(string userEmail);
        Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAsync();
        Task<BaseResponse<AdminDto>> UpdateAsync(UpdateAdminRequestModel updateAdminModel, string id);
        Task DeleteAsync(string superAdminId);
        Task<BaseResponse<IEnumerable<AdminDto>>> SearchAdminByEmailOrUserName(string searchInput);
    }
}
