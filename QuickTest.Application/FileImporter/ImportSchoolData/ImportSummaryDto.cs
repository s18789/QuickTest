using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter.ImportSchoolData
{
    public class ImportSummaryDto
    {
        public int ExistingTeachers { get; set; }
        public int ExistingStudents { get; set; }
        public ExistingRecordsSummary RecordsSummary { get; set; }
        public ImportedGroupsDto ImportedGroupsFromFile { get; set; }

    }
}
