using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.AdminDto;
using SeaBirdProject.Dtos.UserDto;
using SeaBirdProject.Entities;
using SeaBirdProject.GateWay.Email;
using SeaBirdProject.Repositories.Interfaces;
using SeaBirdProject.Services.Interfaces;

namespace SeaBirdProject.Services.Implementations
{
    public class AdminService : IAdminService
    {
      
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _emailSender;
        public AdminService(IAdminRepository adminRepository, IEmailSender emailSender, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _emailSender = emailSender;
            _userRepository = userRepository;
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

        public Task DeleteAsync(string superAdminId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<AdminDto>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<AdminDto>> GetByEmailAsync(string userEmail)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<AdminDto>> GetByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<IEnumerable<AdminDto>>> SearchAdminByEmailOrUserName(string searchInput)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<AdminDto>> UpdateAsync(UpdateAdminRequestModel updateAdminModel, string id)
        {
            throw new NotImplementedException();
        }
    }
}
