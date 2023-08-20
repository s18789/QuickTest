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
        public SchoolDto SchoolDetails { get; set; }
        public List<GroupDto> ImportedGroups { get; set; }
    }
}
