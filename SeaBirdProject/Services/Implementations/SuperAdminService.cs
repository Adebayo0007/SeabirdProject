using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.SuperAdminDto;
using SeaBirdProject.Repositories.Interfaces;
using SeaBirdProject.Services.Interfaces;
using System.Security.Claims;

namespace SeaBirdProject.Services.Implementations
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SuperAdminService(ISuperAdminRepository superAdminRepository, IHttpContextAccessor httpContextAccessor)
        {
            _superAdminRepository = superAdminRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task DeleteAsync(string userId)
        {
            if (userId.Equals(null))
            {
                userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;  // i.e user want to delete his/her account
            }
            var user = _superAdminRepository.GetById(userId);
            if (user.User.IsActive == false) user.User.IsActive = true;
            else user.User.IsActive = false;
            await _superAdminRepository.Delete(user);
        }

        public async Task<BaseResponse<IEnumerable<SuperAdminDto>>> GetAllAsync()
        {
            var superAdmins = await _superAdminRepository.GetAllAsync();
            if(superAdmins.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<SuperAdminDto>>
                {
                    Message = "Super Admins not found",
                    IsSuccess = false,
                };
            }
            var superAdminDto = superAdmins.Select(s => new SuperAdminDto
            {
                Id = s.Id,
                UserId = s.UserId,
                UserName = s.User.UserName,
                Name = s.User.Name,
                PhoneNumber = s.User.PhoneNumber,
                Address = s.User.Address,
                Gender = s.User.Gender,
                Email = s.User.Email,
                Role = s.User.Role,
                IsActive = s.User.IsActive,
                BranchName = s.User.Branch.BranchName,
                BranchType = s.User.Branch.BranchType,
                BranchAddress = s.User.Branch.BranchAddress,
                DateCreated = s.User.DateCreated,
                DateModified = s.User.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<SuperAdminDto>>
            {
                Message = "Super Admins was retrieved successfully",
                IsSuccess = true,
                Data = superAdminDto
            };
        }

        public async Task<BaseResponse<SuperAdminDto>> GetByEmailAsync(string userEmail)
        {
            if(userEmail.Equals(null)) userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            var superAdmin = _superAdminRepository.GetByEmail(userEmail);
            if(superAdmin.Equals(null))
            {
                return new BaseResponse<SuperAdminDto>
                {
                    IsSuccess = false,
                    Message = "No super Admin found"
                };
            }
            var superAdminDto = new SuperAdminDto
            {
                Id = superAdmin.Id,
                UserId = superAdmin.UserId,
                UserName = superAdmin.User.UserName,
                Name = superAdmin.User.Name,
                PhoneNumber = superAdmin.User.PhoneNumber,
                Address = superAdmin.User.Address,
                Gender = superAdmin.User.Gender,
                Email = superAdmin.User.Email,
                Role = superAdmin.User.Role,
                IsActive = superAdmin.User.IsActive,
                BranchName = superAdmin.User.Branch.BranchName,
                BranchType = superAdmin.User.Branch.BranchType,
                BranchAddress = superAdmin.User.Branch.BranchAddress,
                DateCreated = superAdmin.User.DateCreated,
                DateModified = superAdmin.User.DateModified
            };
            return new BaseResponse<SuperAdminDto>
            {
                IsSuccess = true,
                Message = "Super Admin retrieved successfully",
                Data = superAdminDto
            };
        }

        public async Task<BaseResponse<SuperAdminDto>> GetByIdAsync(string userId)
        {
            if (userId.Equals(null)) userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var superAdmin = _superAdminRepository.GetById(userId);
            if (superAdmin.Equals(null))
            {
                return new BaseResponse<SuperAdminDto>
                {
                    IsSuccess = false,
                    Message = "No super Admin found"
                };
            }
            var superAdminDto = new SuperAdminDto
            {
                Id = superAdmin.Id,
                UserId = superAdmin.UserId,
                UserName = superAdmin.User.UserName,
                Name = superAdmin.User.Name,
                PhoneNumber = superAdmin.User.PhoneNumber,
                Address = superAdmin.User.Address,
                Gender = superAdmin.User.Gender,
                Email = superAdmin.User.Email,
                Role = superAdmin.User.Role,
                IsActive = superAdmin.User.IsActive,
                BranchName = superAdmin.User.Branch.BranchName,
                BranchType = superAdmin.User.Branch.BranchType,
                BranchAddress = superAdmin.User.Branch.BranchAddress,
                DateCreated = superAdmin.User.DateCreated,
                DateModified = superAdmin.User.DateModified
            };
            return new BaseResponse<SuperAdminDto>
            {
                IsSuccess = true,
                Message = "Super Admin retrieved successfully",
                Data = superAdminDto
            };
        }

        public async Task<BaseResponse<IEnumerable<SuperAdminDto>>> SearchSuperAdminByEmailOrUserName(string searchInput)
        {
            var searchedSuperAdmin = await _superAdminRepository.SearchSuperAdminByEmailOrUsername(searchInput);
            if (searchedSuperAdmin.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<SuperAdminDto>>
                {
                    Message = "No Super Admin found",
                    IsSuccess = false
                };
            }
            var superAdminDto = searchedSuperAdmin.Select(p => new SuperAdminDto
            {
                Id = p.Id,
                UserId = p.UserId,
                UserName = p.User.UserName,
                Name = p.User.Name,
                PhoneNumber = p.User.PhoneNumber,
                Address = p.User.Address,
                Gender = p.User.Gender,
                Email = p.User.Email,
                Role = p.User.Role,
                IsActive = p.User.IsActive,
                BranchName = p.User.Branch.BranchName,
                BranchType = p.User.Branch.BranchType,
                BranchAddress = p.User.Branch.BranchAddress,
                DateCreated = p.User.DateCreated,
                DateModified = p.User.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<SuperAdminDto>>
            {
                Message = "Super Admin retrieved successfully",
                IsSuccess = true,
                Data = superAdminDto
            };
        }

        public async Task<BaseResponse<SuperAdminDto>> UpdateAsync(UpdateSuperAdminRequestModel updateSuperAdminModel, string id)
        {
            var superAdmin = _superAdminRepository.GetById(id);
            if (superAdmin.Equals(null))
            {
                return new BaseResponse<SuperAdminDto>
                {
                    Message = "Uper Admin does not exist",
                    IsSuccess = false
                };

            }
            superAdmin.User.UserName = updateSuperAdminModel.UserName ?? superAdmin.User.UserName;
            superAdmin.User.Name = updateSuperAdminModel.Name ?? superAdmin.User.Name;
            superAdmin.User.Address = updateSuperAdminModel.Address ?? superAdmin.User.Address;
            superAdmin.User.PhoneNumber = updateSuperAdminModel?.PhoneNumber ?? superAdmin.User.PhoneNumber;
            superAdmin.User.Branch.BranchName = updateSuperAdminModel.BranchName ?? superAdmin.User.Branch.BranchName;
            superAdmin.User.Branch.BranchAddress = updateSuperAdminModel.BranchAddress ?? superAdmin.User.Branch.BranchAddress;
            superAdmin.User.Branch.BranchType = updateSuperAdminModel.BranchType ?? superAdmin.User.Branch.BranchType;
            var superAdminModel = _superAdminRepository.Update(superAdmin);
            var superAdminDto = new SuperAdminDto
            {
                Id = superAdminModel.Id,
                UserId = superAdminModel.UserId,
                UserName = superAdminModel.User.UserName,
                Name = superAdminModel.User.Name,
                PhoneNumber = superAdminModel.User.PhoneNumber,
                Address = superAdminModel.User.Address,
                Gender = superAdminModel.User.Gender,
                Email = superAdminModel.User.Email,
                Password = superAdminModel.User.Password,
                Role = superAdminModel.User.Role,
                IsActive = superAdminModel.User.IsActive,
                BranchName = superAdminModel.User.Branch.BranchName,
                BranchType = superAdmin.User.Branch.BranchType,
                BranchAddress = superAdminModel.User.Branch.BranchAddress,
                DateCreated = superAdminModel.User.DateCreated,
                DateModified = superAdminModel.User.DateModified

            };
            return new BaseResponse<SuperAdminDto>
            {
                Message = "User Updated successfully ",
                IsSuccess = true,
                Data = superAdminDto
            };

        }
    }
}
