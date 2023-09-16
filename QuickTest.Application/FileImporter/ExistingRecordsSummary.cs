using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter
{
    public class ExistingRecordsSummary
    {
        public HashSet<string> ExistingTeacherEmails { get; set; } = new HashSet<string>();
        public HashSet<string> ExistingStudentEmails { get; set; } = new HashSet<string>();
        public HashSet<string> ExistingGroups { get; set; } = new HashSet<string>();
    }
}
