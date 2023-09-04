using QuickTest.Application.Groups;
using QuickTest.Application.Schools;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.FileImporter
{
    public class ImportedGroupsDto
    {
        
        public List<Tuple<string, Teacher, HashSet<Student>>> ImportedGroups { get; set; }
        public int SuccessfulTeacherLoads { get; set; }
        public int FailedTeacherLoads { get; set; }
        public int SuccessfulStudentLoads { get; set; }
        public int FailedStudentLoads { get; set; }
        public int SuccessfulGroupLoads { get; set; }
        public int FailedGroupLoads { get; set; }
        public List<string> ErrorList { get; set; }
    }
}
