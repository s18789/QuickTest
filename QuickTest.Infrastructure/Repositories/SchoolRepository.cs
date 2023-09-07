using Ardalis.Specification;
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
                .AsNoTracking()
                .Where(s => s.Id == id)
                .Include(s => s.Groups)
                .FirstOrDefaultAsync();
        }
        public async Task<School> GetSchoolWithoutGroups(int id)
        {
            return await this.context.Schools
                .AsNoTracking()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }
        public async Task DetachThatMfcker(School school)
        {
            context.Entry(school).State = EntityState.Detached;
        }
        public async Task<int> GetSchoolIdByAdministrator(User user)
        {
            var admin = await this.context.Admins.AsNoTracking().Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            return  admin == null ? 0: admin.SchoolId ?? 0;
        }
        public async Task<School> GetSchoolByUser(User user)
        {
            var userRole = this.context.UserRoles.AsNoTracking().Where(x=> x.Id== user.UserRoleId).FirstOrDefault();
            if (userRole.Name.Equals("student"))
            {
                var studentGroupsId = context.Students.AsNoTracking().Where(x => x.Id == user.Id).Select(y => y.GroupId).ToList();
                var groupsForStudent = new List<Group>();
                foreach (var id in studentGroupsId)
                {
                    var group = context.Groups.AsNoTracking().Where(x => x.Id== id).FirstOrDefault();
                    groupsForStudent.Add(group);
                }
                var anySchool = groupsForStudent.FirstOrDefault();
                var schoolEntity= context.Schools.AsNoTracking().FirstOrDefault(x => x.Id == anySchool.Id) ?? null;
                context.Entry(schoolEntity).State = EntityState.Detached;
                return schoolEntity;
            }else if (userRole.Name.Equals("teacher"))
            {
                var teacherEntity = context.Teachers.AsNoTracking().FirstOrDefault(t => t.Id == user.Id);
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
                var schoolEntity = context.Schools.AsNoTracking().FirstOrDefault(s => s.Id == anyGroup.School.Id) ?? null;
                context.Entry(schoolEntity).State = EntityState.Detached;
                return schoolEntity;

            }
            else if (userRole.Name.Equals("admin"))
            {
                var adminSchool = GetSchoolIdByAdministrator(user);
                var schoolEntity = context.Schools.AsNoTracking().Where(x => x.Id == adminSchool.Id).FirstOrDefault() ?? null;
                context.Entry(schoolEntity).State = EntityState.Detached;
                return schoolEntity;
            }
            else
            {
                return null;
            }

        }

    }
}
