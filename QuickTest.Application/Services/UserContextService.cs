using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Services;

namespace QuickTest.Application.Services
{
    public sealed class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<User> userManager;

        public UserContextService(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<User> GetUserContextAsync()
        {
            var userClaimsPrincipal = this.httpContextAccessor.HttpContext.User;

            return await this.userManager.GetUserAsync(userClaimsPrincipal);
        }
    }
}
