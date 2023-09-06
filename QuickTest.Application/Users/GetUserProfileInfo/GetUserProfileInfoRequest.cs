using MediatR;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.GetUserProfileInfo
{
    public class GetUserProfileInfoRequest : IRequest<UserProfileInfoDto>
    {
        public int UserId { get; set; }
    }
}
