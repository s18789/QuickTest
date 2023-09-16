using MediatR;

namespace QuickTest.Application.Users.GetUserProfileInfo
{
    public sealed class GetUserProfileInfoRequest : IRequest<UserProfileInfoDto>
    {
    }
}
