using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using System.Security.Claims;

namespace QuickTest.Application.Users.ChangeUserPassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordRequest, ChangePasswordResponseDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangePasswordHandler(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ChangePasswordResponseDto> Handle(ChangePasswordRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new ChangePasswordResponseDto { IsSuccess = false, Message = "User not found" };
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.ChangePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                return new ChangePasswordResponseDto { IsSuccess = false, Message = "Password change failed" };
            }

            return new ChangePasswordResponseDto { IsSuccess = true, Message = "Password changed successfully" };
        }
    }
}
