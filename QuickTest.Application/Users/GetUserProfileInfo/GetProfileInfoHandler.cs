using MediatR;
using QuickTest.Application.Services;

namespace QuickTest.Application.Users.GetUserProfileInfo
{
    public sealed class GetUserProfileInfoHandler : IRequestHandler<GetUserProfileInfoRequest, UserProfileInfoDto>
    {
        private readonly IUserContextService userContextService;

        public GetUserProfileInfoHandler(IUserContextService userContextService)
        {
            this.userContextService = userContextService;
        }

        public async Task<UserProfileInfoDto> Handle(GetUserProfileInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await this.userContextService.GetUserContextAsync();

            if (user == null)
            {
                return null;
            }

            return new UserProfileInfoDto
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };
        }
    }
}
