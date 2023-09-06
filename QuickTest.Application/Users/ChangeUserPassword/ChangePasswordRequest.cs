using MediatR;
using QuickTest.Application.Users.CreateUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.ChangeUserPassword
{
    public class ChangePasswordRequest : IRequest<ChangePasswordResponseDto>
    {
        public ChangePasswordDto ChangePasswordDto { get; set; }
    }
}
