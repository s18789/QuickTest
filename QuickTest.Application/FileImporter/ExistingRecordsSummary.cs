using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter
{
    public class ExistingRecordsSummary
    {
        public List<string> ExistingTeacherEmails { get; set; } = new List<string>();
        public List<string> ExistingStudentEmails { get; set; } = new List<string>();
        public List<string> ExistingGroups { get; set; } = new List<string>();
    }
}
