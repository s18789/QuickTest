using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.CreateUser
{
    public class CreateUserRequest : IRequest<ResponseDto>
    {
        public CreateUserDto UserDto { get; set; }
    }
}
