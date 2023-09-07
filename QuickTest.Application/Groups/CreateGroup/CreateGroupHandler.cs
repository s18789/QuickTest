using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;

namespace QuickTest.Application.Groups.CreateGroup;

public class CreateGroupHandler : IRequestHandler<CreateGroupRequest, GroupDto>
{
    private readonly IGroupRepository groupRepository;
    private readonly IMapper mapper;
    private readonly ITeacherRepository teacherRepository;

    public CreateGroupHandler(IGroupRepository groupRepository, IMapper mapper, ITeacherRepository teacherRepository)
    {
        this.groupRepository = groupRepository;
        this.mapper = mapper;
        this.teacherRepository = teacherRepository;
    }
    public async Task<GroupDto> Handle(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        var group = mapper.Map<Group>(request.Group);

        if (group.GroupTeachers?.Any() == true)
        {
            return null;
        }

        await this.groupRepository.AddAsync(group);

        return mapper.Map<GroupDto>(group);
    }

    public async Task<GroupDto> HandleGroupTeacher(CreateGroupRequest request, CancellationToken cancellationToken)
    {
        Group groupToAdd = new Group();

        try
        {
            groupToAdd = mapper.Map<Group>(request.Group);
        }
        catch (Exception ex)
        {
            throw ex;
        }

        if (groupToAdd.GroupTeachers?.Any() == false)
        {
            return null;
        }

        groupRepository.DetachThatMfcker(groupToAdd);

        List<GroupTeacher> groupTeachersToAdd = new List<GroupTeacher>();

        foreach (var groupTeacher in groupToAdd.GroupTeachers)
        {
            var teacher = await teacherRepository.GetTeacherWithoutGroups(groupTeacher.Teacher.Id);
            if (teacher == null)
            {
                continue;
            }

            GroupTeacher newGroupTeacher = new GroupTeacher
            {
                TeacherId = groupTeacher.Teacher.Id,
                GroupId = groupTeacher.Group.Id
            };

            groupTeachersToAdd.Add(newGroupTeacher);
        }

        groupToAdd.GroupTeachers.Clear();

        groupToAdd.GroupTeachers = groupTeachersToAdd;

        try
        {
            foreach (var groupTeacher in groupTeachersToAdd)
            {
                await this.groupRepository.AddGroupTeacher(groupTeacher);
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return mapper.Map<GroupDto>(groupToAdd);
    }
}
