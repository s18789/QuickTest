using MediatR;
using QuickTest.Application.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Schools.GetSchool
{
    public class GetSchoolRequest : IRequest<SchoolDto>
    {
        public int Id { get; set; }
    }
}
