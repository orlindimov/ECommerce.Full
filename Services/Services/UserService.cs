using AutoMapper;
using Core.Dtos.Users;
using Core.Models;
using Core.Responses;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<RoleApp> _roleManager;
        private readonly IMapper _mapper;

        public UserService(UserManager<UserApp> userManager, RoleManager<RoleApp> roleManager,  IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseDto<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp { Email = createUserDto.Email, UserName = createUserDto.UserName };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return CustomResponseDto<UserAppDto>.Fail(400, "Account could not created");
            }
            return CustomResponseDto<UserAppDto>.Success(200, _mapper.Map<UserAppDto>(user));
        }

        public async Task<CustomResponseDto<NoContentDto>> CreateUserRoles(string userName)
        {
            if (!await _roleManager.RoleExistsAsync("admin"))
            {
                await _roleManager.CreateAsync(new() { Name = "admin" });
                await _roleManager.CreateAsync(new() { Name = "manager" });

            }
            var user = await _userManager.FindByNameAsync(userName);

            await _userManager.AddToRoleAsync(user, "admin");
            await _userManager.AddToRoleAsync(user, "manager");

            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status201Created);
        }

        public async Task<CustomResponseDto<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return CustomResponseDto<UserAppDto>.Fail(404, "UserName not found");
            }

            return CustomResponseDto<UserAppDto>.Success(200, _mapper.Map<UserAppDto>(user));
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdatePassword(ResetPasswordDto resetPasswordDto)
        {
            
            var user=await _userManager.FindByIdAsync(resetPasswordDto.Id);

            if (user == null)
            {
                throw new Exception("user could not found");
            }
            var resetToken=await _userManager.GeneratePasswordResetTokenAsync(user);

            if (resetToken==null)
            {
                throw new Exception("Error while generating reset token");
            }
            var result=await _userManager.ResetPasswordAsync(user, resetToken,resetPasswordDto.Password);

            if(!result.Succeeded)
            {
                return CustomResponseDto<NoContentDto>.Fail(404,"error");
            }
            await _userManager.UpdateSecurityStampAsync(user);
            
            return CustomResponseDto<NoContentDto>.Success(204);
        }
    }
}
