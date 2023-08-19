using MediatR;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Schools.GetSchool
{
    public class GetSchoolHandler : IRequestHandler<GetSchoolRequest, SchoolDto>
    {
        private readonly ISchoolRepository schoolRepository;

        public GetSchoolHandler(ISchoolRepository schoolRepository)
        {
            this.schoolRepository = schoolRepository;
        }

        public async Task<SchoolDto> Handle(GetSchoolRequest request, CancellationToken cancellationToken)
        {
            var school = await this.schoolRepository.GetSchoolIncludeGroups(request.Id);

            return new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Address = school.Address,
                Groups = school.Groups
            };
        }
    }
}
