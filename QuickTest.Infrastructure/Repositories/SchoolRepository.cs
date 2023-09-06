using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Repositories
{
    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {
        private readonly DataContext context;

        public SchoolRepository(DataContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public async Task<School> GetSchoolIncludeGroups(int id)
        {
            return await this.context.Schools
                .Where(s => s.Id == id)
                .Include(s => s.Groups)
                .FirstOrDefaultAsync();
        }
        public async Task<int> GetSchoolIdByAdministrator(User user)
        {
            var admin = await this.context.Admins.Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            return  admin == null ? 0: admin.SchoolId ?? 0;
        }
        public async Task<School> GetSchoolByUser(User user)
        {
            var userRole = this.context.UserRoles.Where(x=> x.Id== user.UserRoleId).FirstOrDefault();
            if (userRole.Name.Equals("student"))
            {
                var studentGroupsId = context.Students.Where(x => x.Id == user.Id).Select(y => y.GroupId).ToList();
                var groupsForStudent = new List<Group>();
                foreach (var id in studentGroupsId)
                {
                    var group = context.Groups.Where(x => x.Id== id).FirstOrDefault();
                    groupsForStudent.Add(group);
                }
                var anySchool = groupsForStudent.FirstOrDefault();
                return context.Schools.FirstOrDefault(x => x.Id == anySchool.Id)?? null;

            }else if (userRole.Name.Equals("teacher"))
            {
                var teacherEntity = context.Teachers.FirstOrDefault(t => t.Id == user.Id);
                if (teacherEntity != null)
                {
                    context.Entry(teacherEntity)
                           .Collection(t => t.GroupTeachers)
                           .Query()
                           .Include(gt => gt.Group)
                           .Load();
                }

                var anyGroup = teacherEntity.GroupTeachers.Select(gt => gt.Group).FirstOrDefault();
                if (anyGroup == null)
                {
                    return null; 
                }

                return context.Schools.FirstOrDefault(s => s.Id == anyGroup.School.Id) ?? null;

            }
            else if (userRole.Name.Equals("admin"))
            {
                var adminSchool = GetSchoolIdByAdministrator(user);
                return context.Schools.Where(x => x.Id == adminSchool.Id).FirstOrDefault()?? null;
            }
            else
            {
                return null;
            }

        }

    }
}
