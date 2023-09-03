using MediatR;
using QuickTest.Application.FileImporter.ImportSchoolData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.BulkImport
{
    public class BulkImportHandlerRequest : IRequest<CreatedAccountsSummary>
    {
        public ImportSummaryDto ImportSummary { get; set; }
        public int SchoolId { get; set; }
    }
}
