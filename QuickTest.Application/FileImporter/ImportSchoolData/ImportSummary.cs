using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.ImportSchoolData
{
    public class ImportSummary
    {
        public int TotalRecords { get; set; }
        public int SuccessfulInserts { get; set; }
        public int FailedInserts { get; set; }
        public List<string> FailedRecords { get; set; } 
    }
}
