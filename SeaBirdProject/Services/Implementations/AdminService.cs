using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.AdminDto;
using SeaBirdProject.Dtos.SuperAdminDto;
using SeaBirdProject.Entities;
using SeaBirdProject.GateWay.Email;
using SeaBirdProject.Repositories.Implementations;
using SeaBirdProject.Repositories.Interfaces;
using SeaBirdProject.Services.Interfaces;
using System.Security.Claims;

namespace SeaBirdProject.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AdminService(IAdminRepository adminRepository, IEmailSender emailSender,
            IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                _adminRepository = adminRepository;
                _emailSender = emailSender;
                _userRepository = userRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            catch (Exception ex) 
            {
                throw new Exception($"The exception message says: {ex.Message}");
            }
        }
        public async Task<BaseResponse<AdminDto>> CreateAsync(CreateAdminRequestModel createAdminModel)
        {
            var userExist = _userRepository.ExistByEmailAsync(createAdminModel.Email);            
            if (userExist.Equals(true))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSuccess = false,
                    Message = "Email already exist",
                };
            }

            var response = await _emailSender.EmailValidaton(createAdminModel.Email);
            if (response.Equals(false))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSuccess = false,
                    Message = "Your email is not valid,please check.",
                };
            }

            var branch = new Branch
            {
               BranchAddress = createAdminModel.BranchAddress,
               BranchName = createAdminModel.BranchName,
               BranchType = createAdminModel.BranchType,
            };

            var user = new User
            {
                BranchId = branch.Id,
                UserName = createAdminModel.UserName,
                ProfilePicture = createAdminModel.ProfilePicture,
                Name = $"{createAdminModel.FirstName} {createAdminModel.LastName}",
                PhoneNumber = createAdminModel.PhoneNumber,
                Address = createAdminModel.Address,
                Gender = createAdminModel.Gender,
                Email = createAdminModel.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(createAdminModel.Password),
                Role = "Admin",
                IsActive = false,
                IsRegistered = false,
                Branch = branch,
                DateCreated = DateTime.Now
            };

            var admin = new Admin
            {
                UserId = user.Id,
                User = user
            };
             var adminModel = await _adminRepository.CreateAsync(admin);

            var adminDto = new AdminDto
            {
                Id = adminModel.Id,
                UserId = adminModel.UserId,
                UserName = adminModel.User.UserName,
                Name = adminModel.User.Name,
                PhoneNumber = adminModel.User.PhoneNumber,
                Address = adminModel.User.Address,
                Gender = adminModel.User.Gender,
                Email = adminModel.User.Email,
                Role = adminModel.User.Role,
                IsActive = adminModel.User.IsActive,
                BranchName = adminModel.User.Branch.BranchName,
                BranchType = adminModel.User.Branch.BranchType,
                BranchAddress = adminModel.User.Branch.BranchAddress,
                DateCreated = adminModel.User.DateCreated,
                DateModified = adminModel.User.DateModified
            };
            return new BaseResponse<AdminDto>
            {
                IsSuccess = true,
                Message = "Admin created successfully.",
                Data = adminDto
            };
        }

        public async Task DeleteAsync(string userId)
        {
          var admin =  _adminRepository.GetById(userId);
            admin.User.IsActive = false;
            await _adminRepository.Delete(admin);
        }

        public async Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            if (admins.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<AdminDto>>
                {
                    Message = "There is no Admin available",
                    IsSuccess = false,
                };
            }
            var adminDto = admins.Select(a => new AdminDto
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.User.UserName,
                Name =  a.User.Name,
                PhoneNumber = a.User.PhoneNumber,
                Address = a.User.Address,
                Gender = a.User.Gender,
                Email = a.User.Email,
                Role = a.User.Role,
                IsActive = a.User.IsActive,
                BranchName = a.User.Branch.BranchName,
                BranchType = a.User.Branch.BranchType,
                BranchAddress = a.User.Branch.BranchAddress,
                DateCreated = a.User.DateCreated,
                DateModified = a.User.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<AdminDto>>
            {
                Message = "Admins was retrieved successfully",
                IsSuccess = true,
                Data = adminDto
            };
        }

        public async Task<BaseResponse<AdminDto>> GetByEmailAsync(string userEmail)
        {
            if (userEmail.Equals(null)) userEmail = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            var admin = _adminRepository.GetByEmail(userEmail);
            if (admin.Equals(null))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSuccess = false,
                    Message = "No Admin found"
                };
            }
            var adminDto = new AdminDto
            {
                Id = admin.Id,
                UserId = admin.UserId,
                UserName = admin.User.UserName,
                Name = admin.User.Name,
                PhoneNumber = admin.User.PhoneNumber,
                Address = admin.User.Address,
                Gender = admin.User.Gender,
                Email = admin.User.Email,
                Role = admin.User.Role,
                IsActive = admin.User.IsActive,
                BranchName = admin.User.Branch.BranchName,
                BranchType = admin.User.Branch.BranchType,
                BranchAddress = admin.User.Branch.BranchAddress,
                DateCreated = admin.User.DateCreated,
                DateModified = admin.User.DateModified
            };
            return new BaseResponse<AdminDto>
            {
                IsSuccess = true,
                Message = "Admin retrieved successfully",
                Data = adminDto
            };
        }

        public async Task<BaseResponse<AdminDto>> GetByIdAsync(string userId)
        {
            if (userId.Equals(null)) userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var admin = _adminRepository.GetById(userId);
            if (admin.Equals(null))
            {
                return new BaseResponse<AdminDto>
                {
                    IsSuccess = false,
                    Message = "No Admin found"
                };
            }
            var adminDto = new AdminDto
            {
                Id = admin.Id,
                UserId = admin.UserId,
                UserName = admin.User.UserName,
                Name = admin.User.Name,
                PhoneNumber = admin.User.PhoneNumber,
                Address = admin.User.Address,
                Gender = admin.User.Gender,
                Email = admin.User.Email,
                Role = admin.User.Role,
                IsActive = admin.User.IsActive,
                BranchName = admin.User.Branch.BranchName,
                BranchType = admin.User.Branch.BranchType,
                BranchAddress = admin.User.Branch.BranchAddress,
                DateCreated = admin.User.DateCreated,
                DateModified = admin.User.DateModified
            };
            return new BaseResponse<AdminDto>
            {
                IsSuccess = true,
                Message = "Admin retrieved successfully",
                Data = adminDto
            };
        }

        public async Task<BaseResponse<IEnumerable<AdminDto>>> SearchAdminByEmailOrUserName(string searchInput)
        {
            var searchedAdmin = await _adminRepository.SearchAdminByEmailOrUsername(searchInput);
            if (searchedAdmin.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<AdminDto>>
                {
                    Message = "No Admin found",
                    IsSuccess = false
                };
            }
            var adminDto = searchedAdmin.Select(a => new AdminDto
            {
                Id = a.Id,
                UserId = a.UserId,
                UserName = a.User.UserName,
                Name = a.User.Name,
                PhoneNumber = a.User.PhoneNumber,
                Address = a.User.Address,
                Gender = a.User.Gender,
                Email = a.User.Email,
                Role = a.User.Role,
                IsActive = a.User.IsActive,
                BranchName = a.User.Branch.BranchName,
                BranchType = a.User.Branch.BranchType,
                BranchAddress = a.User.Branch.BranchAddress,
                DateCreated = a.User.DateCreated,
                DateModified = a.User.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<AdminDto>>
            {
                Message = "Admin retrieved successfully",
                IsSuccess = true,
                Data = adminDto
            };
        }

        public async Task<BaseResponse<AdminDto>> UpdateAsync(UpdateAdminRequestModel updateAdminModel, string id)
        {
            if (id == null) id = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var admin = _adminRepository.GetById(id);
            if (admin.Equals(null))
            {
                return new BaseResponse<AdminDto>
                {
                    Message = "Admin does not exist",
                    IsSuccess = false
                };

            }
            admin.User.UserName = updateAdminModel.UserName ?? admin.User.UserName;
            admin.User.Name = updateAdminModel.Name ?? admin.User.Name;
            admin.User.Address = updateAdminModel.Address ?? admin.User.Address;
            admin.User.PhoneNumber = updateAdminModel?.PhoneNumber ?? admin.User.PhoneNumber;
            admin.User.Branch.BranchName = updateAdminModel.BranchName ?? admin.User.Branch.BranchName;
            admin.User.Branch.BranchAddress = updateAdminModel.BranchAddress ?? admin.User.Branch.BranchAddress;
            admin.User.Branch.BranchType = updateAdminModel.BranchType ?? admin.User.Branch.BranchType;
            var adminModel = _adminRepository.Update(admin);
            var adminDto = new AdminDto
            {
                Id = adminModel.Id,
                UserId = adminModel.UserId,
                UserName = adminModel.User.UserName,
                Name = adminModel.User.Name,
                PhoneNumber = adminModel.User.PhoneNumber,
                Address = adminModel.User.Address,
                Gender = adminModel.User.Gender,
                Email = adminModel.User.Email,
                Password = adminModel.User.Password,
                Role = adminModel.User.Role,
                IsActive = adminModel.User.IsActive,
                BranchName = adminModel.User.Branch.BranchName,
                BranchType = adminModel.User.Branch.BranchType,
                BranchAddress = adminModel.User.Branch.BranchAddress,
                DateCreated = adminModel.User.DateCreated,
                DateModified = adminModel.User.DateModified

            };
            return new BaseResponse<AdminDto>
            {
                Message = "Adin Updated successfully ",
                IsSuccess = true,
                Data = adminDto
            };

        }
    }
}
