using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.SuperAdminDto;

namespace SeaBirdProject.Services.Interfaces
{
    public interface ISuperAdminService
    {
        Task<BaseResponse<SuperAdminDto>> GetByIdAsync(string userId);
        Task<BaseResponse<SuperAdminDto>> GetByEmailAsync(string userEmail);
        Task<BaseResponse<IEnumerable<SuperAdminDto>>> GetAllAsync();
        Task<BaseResponse<SuperAdminDto>> UpdateAsync(UpdateSuperAdminRequestModel updateSuperAdminModel, string id);
        Task DeleteAsync(string superAdminId);
        Task<BaseResponse<IEnumerable<SuperAdminDto>>> SearchSuperAdminByEmailOrUserName(string searchInput);
    }
}
