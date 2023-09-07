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
        Task<School> GetSchoolWithoutGroups(int id);
        Task<int> GetSchoolIdByAdministrator(User user);
        Task<School> GetSchoolByUser(User user);
        Task DetachThatMfcker(School school);
    }
}
