using Core.Dtos.Users;
using Core.Responses;

namespace Core.Services
{
    public interface IUserService
    {
        Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName);

        Task<CustomResponseDto<NoContentDto>> CreateUserRoles(string userName);

        Task<CustomResponseDto<NoContentDto>> UpdatePassword(ResetPasswordDto resetPasswordDto);
    }
}
