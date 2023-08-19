using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.ImportSchoolData
{
    public class ImportSchoolDataRequest : IRequest<ImportSchoolDataResponse>
    {
        public IFormFile File { get; set; }
    }
}
