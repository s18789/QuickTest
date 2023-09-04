using QuickTest.Application.FileImporter.ImportSchoolData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.BulkImport
{
    public class BulkImportRequest
    {
        public ImportSummaryDto ImportSummary { get; set; }
        public int SchoolId { get; set; }
    }
}
