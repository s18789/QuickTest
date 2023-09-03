using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter
{
    public class CreatedAccountsSummary
    {
        public int TeachersCreated { get; set; }
        public int StudentsCreated { get; set; }
        public int TeacherCreationFailed { get; set; }
        public int StudentCreationFailed { get; set; }
        public int GroupsCreated { get; set; }
        public int GroupsFailed { get; set;}
        public List<string> ErrorList { get; set; }
        public bool IsSuccess { get; set; }
    }
}
