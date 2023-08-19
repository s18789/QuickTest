using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.Students.AddStudentToGroup
{
    public class AddStudentToGroupHandler : IRequestHandler<AddStudentToGroupsRequest, bool>
    {
        private readonly IGroupRepository groupRepository;
        private readonly IStudentRepository studentRepository;

        public AddStudentToGroupHandler(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            this.groupRepository = groupRepository;
            this.studentRepository = studentRepository;
        }

        public async Task<bool> Handle(AddStudentToGroupsRequest request, CancellationToken cancellationToken)
        {
            var student = await this.studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
            {
                return false;
            }

            foreach (var groupId in request.GroupIds)
            {
                var group = await this.groupRepository.GetByIdAsync(groupId);
                if (group == null)
                {
                    continue;
                }

                group.Students.Add(student);
                await this.groupRepository.UpdateAsync(group);
            }

            return true;
        }
    }
}
