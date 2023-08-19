using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Interfaces
{
    public interface ISchoolRepository
    {
        Task<School> GetSchoolIncludeGroups(int id);
    }
}
