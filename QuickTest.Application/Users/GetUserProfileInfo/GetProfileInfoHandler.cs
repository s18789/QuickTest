using MediatR;
using QuickTest.Application.Schools;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Users.GetUserProfileInfo
{
    public class GetUserProfileInfoHandler : IRequestHandler<GetUserProfileInfoRequest, UserProfileInfoDto>
    {
        private readonly IUserRepository userRepository;
        private readonly ISchoolRepository schoolRepository;

        public GetUserProfileInfoHandler(IUserRepository userRepository, ISchoolRepository schoolRepository)
        {
            this.userRepository = userRepository;
            this.schoolRepository = schoolRepository;
        }

        public async Task<UserProfileInfoDto> Handle(GetUserProfileInfoRequest request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetUserByIdAsync(request.UserId);
            if (user == null)
            {
                return null;
            }

            var school = await schoolRepository.GetSchoolByUser(user);
            if (school == null)
            {
                return new UserProfileInfoDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    School = new SchoolDto
                    {
                        Name = "No school is attached to this user"
                    }
                };
            }
            else
            {
                return new UserProfileInfoDto
                {
                    UserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    School = new SchoolDto
                    {
                        Id = school.Id,
                        Name = school.Name
                    }
                };
            }

            
        }
    }
}
