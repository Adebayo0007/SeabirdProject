using SeaBirdProject.Dtos;
using SeaBirdProject.Dtos.UserDto;
using SeaBirdProject.Repositories.Interfaces;
using SeaBirdProject.Services.Interfaces;

namespace SeaBirdProject.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        public async Task DeleteAsync(string userId)
        {
           var user = _userRepository.GetById(userId);
            user.IsActive = false;
            await _userRepository.Delete(user);
        }

        public async Task<bool> ExistByEmailAsync(string userEmail)
        {
            return await _userRepository.ExistByEmailAsync(userEmail);
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> GetAllAsync()
        {
           var users = await _userRepository.GetAllAsync();
            if (users.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "No user found ⚠",
                    IsSuccess = false
                };
            }
            var userDto = users.Select(u => new UserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                ProfilePicture = u.ProfilePicture,
                Name = u.Name,
                PhoneNumber = u.PhoneNumber,
                Address = u.Address,
                Gender = u.Gender,
                Email = u.Email,
                Role = u.Role,
                IsActive = u.IsActive,
                IsRegistered = u.IsRegistered,
                BranchName = u.Branch.BranchName,
                BranchType = u.Branch.BranchType,
                BranchAddress = u.Branch.BranchAddress,
                DateCreated = u.DateCreated,
                DateModified = u.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "users found successfully",
                IsSuccess = true,
                Data = userDto
            };
        }

        public async Task<BaseResponse<UserDto>> GetByEmailAsync(string userEmail)
        {
            var userExist = await _userRepository.ExistByEmailAsync(userEmail);
            if (userExist.Equals(false))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "user does not exist ⚠",
                    IsSuccess = false,
                    Data = null
                };
            }
            var user = _userRepository.GetByEmail(userEmail);
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                IsRegistered = user.IsRegistered,
                BranchName = user.Branch.BranchName,
                BranchType = user.Branch.BranchType,
                BranchAddress = user.Branch.BranchAddress,
                DateCreated = user.DateCreated,
                DateModified = user.DateModified

            };
            return new BaseResponse<UserDto>
            {
                Message = "User found successfully",
                IsSuccess = true,
                Data = userDto
            };
        }

        public async Task<BaseResponse<UserDto>> GetByIdAsync(string userId)
        {
            var user = _userRepository.GetById(userId);
            if(user.Equals(null))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist with that id ⚠",
                    IsSuccess = false,
                    Data = null
                };
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                IsRegistered = user.IsRegistered,
                BranchName = user.Branch.BranchName,
                BranchType = user.Branch.BranchType,
                BranchAddress = user.Branch.BranchAddress,
                DateCreated = user.DateCreated,
                DateModified = user.DateModified

            };
            return new BaseResponse<UserDto>
            {
                Message = "user found successfully",
                IsSuccess = true,
                Data = userDto
            };
        }

        public async Task<BaseResponse<UserDto>> Login(LoginRequestModel logInRequestModel)
        {
            var userExistByEmail = await  _userRepository.ExistByEmailAsync(logInRequestModel.Email);
            if(userExistByEmail.Equals(false)) 
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Email does not exist,Check youe input ⚠",
                    IsSuccess = false,
                    Data = null
                };
            }
            var user = _userRepository.GetByEmail(logInRequestModel.Email);
            if(user.Equals(null)) 
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist,Check youe input ⚠",
                    IsSuccess = false,
                    Data = null
                };
            }
            if(user.IsRegistered.Equals(false))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "You are yet to be verified by the Admin,Check back later",
                    IsSuccess = false,
                    Data = null
                };
            }
            if(user.IsActive.Equals(false))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "You are not an active user of the Application,Visit your Admin",
                    IsSuccess = false,
                    Data = null
                };

            }
            var password = BCrypt.Net.BCrypt.Verify(logInRequestModel.Password, user.Password);
            if(userExistByEmail.Equals(false) || password.Equals(false))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "Wrong email or password,Check your input ⚠",
                    IsSuccess = false,
                    Data = null
                };

            }
            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Gender = user.Gender,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                IsRegistered = user.IsRegistered,
                BranchName = user.Branch.BranchName,
                BranchType = user.Branch.BranchType,
                BranchAddress = user.Branch.BranchAddress,
                DateCreated = user.DateCreated,
                DateModified = user.DateModified

            };
            return new BaseResponse<UserDto>
            {
                Message = "Login successfully ",
                IsSuccess = true,
                Data = userDto
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> PendingRegistration()
        {
            var pendingRegistration = await  _userRepository.PendingRegistration();
            if (pendingRegistration.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "There is no pending registration yet",
                    IsSuccess = false,
                    Data = null
                };
            }
            var userDto = pendingRegistration.Select(p => new UserDto {
                Id = p.Id,
                UserName = p.UserName,
                ProfilePicture = p.ProfilePicture,
                Name = p.Name,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                Gender = p.Gender,
                Email = p.Email,
                Role = p.Role,
                IsActive = p.IsActive,
                IsRegistered = p.IsRegistered,
                BranchName = p.Branch.BranchName,
                BranchType = p.Branch.BranchType,
                BranchAddress = p.Branch.BranchAddress,
                DateCreated = p.DateCreated,
                DateModified = p.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "List of the pending registration that are yet to be verified",
                IsSuccess = true,
                Data = userDto
            };
        }

        public async Task<BaseResponse<IEnumerable<UserDto>>> SearchUserByEmailOrUserName(string searchInput)
        {
           var searchedUser = await _userRepository.SearchUserByEmailOrUsername(searchInput);
            if (searchedUser.Count().Equals(0))
            {
                return new BaseResponse<IEnumerable<UserDto>>
                {
                    Message = "No User found",
                    IsSuccess = false
                };
            }
            var userDto = searchedUser.Select(p => new UserDto
            {
                Id = p.Id,
                UserName = p.UserName,
                ProfilePicture = p.ProfilePicture,
                Name = p.Name,
                PhoneNumber = p.PhoneNumber,
                Address = p.Address,
                Gender = p.Gender,
                Email = p.Email,
                Role = p.Role,
                IsActive = p.IsActive,
                IsRegistered = p.IsRegistered,
                BranchName = p.Branch.BranchName,
                BranchType = p.Branch.BranchType,
                BranchAddress = p.Branch.BranchAddress,
                DateCreated = p.DateCreated,
                DateModified = p.DateModified
            }).ToList();
            return new BaseResponse<IEnumerable<UserDto>>
            {
                Message = "Users retrieved successfully",
                IsSuccess = true,
                Data = userDto
            };
        }

        public BaseResponse<UserDto> UpdateAsync(UpdateUserRequestModel updateUserModel, string id)
        {
            var user = _userRepository.GetById(id);
            if(user.Equals(null))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist",
                    IsSuccess = false
                };

            }
            user.UserName = updateUserModel.UserName ?? user.UserName;
            user.Name = updateUserModel.Name ?? user.Name;
            user.Address = updateUserModel.Address ?? user.Address;
            user.PhoneNumber = updateUserModel?.PhoneNumber ?? user.PhoneNumber;
            user.Branch.BranchName = updateUserModel.BranchName ?? user.Branch.BranchName;
            user.Branch.BranchAddress = updateUserModel.BranchAddress ?? user.Branch.BranchAddress;
            user.Branch.BranchType = updateUserModel.BranchType ?? user.Branch.BranchType;
           var userr = _userRepository.Update(user);
            var userDto = new UserDto
            {
                Id = userr.Id,
                UserName = userr.UserName,
                ProfilePicture = userr.ProfilePicture,
                Name = userr.Name,
                PhoneNumber = userr.PhoneNumber,
                Address = userr.Address,
                Gender = userr.Gender,
                Email = userr.Email,
                Password = userr.Password,
                Role = userr.Role,
                IsActive = userr.IsActive,
                IsRegistered = userr.IsRegistered,
                BranchName = userr.Branch.BranchName,
                BranchType = user.Branch.BranchType,
                BranchAddress = userr.Branch.BranchAddress,
                DateCreated = userr.DateCreated,
                DateModified = userr.DateModified

            };
            return new BaseResponse<UserDto>
            {
                Message = "User Updated successfully ",
                IsSuccess = true,
                Data = userDto
            };

        }

        public BaseResponse<UserDto> UpdatePassword(ForgetPasswordRequestModel model)
        {
            var user = _userRepository.GetByEmail(model.Email);
            if (user.Equals(null)) 
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User does not exist",
                    IsSuccess = false
                };
            }
            user.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            _userRepository.Update(user);
            return new BaseResponse<UserDto>
            {
                Message = "Password Updated successfully",
                IsSuccess = true
            };
        }

        public BaseResponse<UserDto> VerifyUser(string userEmail)
        {
            var user = _userRepository.GetByEmail(userEmail); 
            if(user.Equals(null))
            {
                return new BaseResponse<UserDto>
                {
                    Message = "User cant be found 🙄",
                    IsSuccess = false,
                };
            }
            user.IsRegistered = true;
            user.IsActive = true;
            var userr = _userRepository.Update(user);

            var userDto = new UserDto
            {
                Id = userr.Id,
                UserName = userr.UserName,
                ProfilePicture = userr.ProfilePicture,
                Name = userr.Name,
                PhoneNumber = userr.PhoneNumber,
                Address = userr.Address,
                Gender = userr.Gender,
                Email = userr.Email,
                Password = userr.Password,
                Role = userr.Role,
                IsActive = userr.IsActive,
                IsRegistered = userr.IsRegistered,
                BranchName = userr.Branch.BranchName,
                BranchType = user.Branch.BranchType,
                BranchAddress = userr.Branch.BranchAddress,
                DateCreated = userr.DateCreated,
                DateModified = userr.DateModified

            };
            return new BaseResponse<UserDto>
            {
                Message = "User Updated successfully ",
                IsSuccess = true,
                Data = userDto
            };

        }
    }
}
