using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Teachers.AddTeacherToGroups
{
    public class AddTeacherToGroupsHandler : IRequestHandler<AddTeacherToGroupsRequest, bool>
    {
        private readonly IGroupRepository groupRepository;
        private readonly ITeacherRepository teacherRepository;

        public AddTeacherToGroupsHandler(IGroupRepository groupRepository, ITeacherRepository teacherRepository)
        {
            this.groupRepository = groupRepository;
            this.teacherRepository = teacherRepository;
        }

        public async Task<bool> Handle(AddTeacherToGroupsRequest request, CancellationToken cancellationToken)
        {
            var teacher = await this.teacherRepository.GetByIdAsync(request.TeacherId);

            if (teacher == null)
            {
                return false;
            }

            foreach (var groupId in request.GroupIds)
            {
                var group = await this.groupRepository.GetByIdAsync(groupId);
                if (group == null)
                {
                    return false;
                }

                var groupTeacher = new GroupTeacher
                {
                    TeacherId = teacher.Id,
                    GroupId = group.Id
                };

                if (group.GroupTeachers == null)
                {
                    group.GroupTeachers = new List<GroupTeacher>();
                }

                group.GroupTeachers.Add(groupTeacher);

                await this.groupRepository.UpdateAsync(group);
            }

            return true;
        }
    }
}
