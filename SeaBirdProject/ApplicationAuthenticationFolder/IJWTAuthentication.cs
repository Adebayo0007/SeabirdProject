using SeaBirdProject.Dtos.UserDto;
using SeaBirdProject.Dtos;

namespace SeaBirdProject.ApplicationAuthenticationFolder
{
    public interface IJWTAuthentication
    {
        string GenerateToken(BaseResponse<UserDto> model);
    }
}
