using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;
public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(DataContext context) : base(context)
    {
    }

    public async Task<bool> CheckIfGroupExists(string groupName)
    {
        var existingGroup =  this.context.Groups.FirstOrDefault(x => x.Name.Equals(groupName));
        return existingGroup == null? false: true;
    }
    public async Task<Group> CreateGropuByName(string groupName, School school)
    {
        var groupToInsert = new Group
        {
            Name = groupName,
            School = school
        };

        try
        {
            await context.Groups.AddAsync(groupToInsert);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Group was not created because an error occured: "+ex.ToString());
        }

        return groupToInsert;
    }
    public async Task DetachThatMfcker(Group group)
    {
        context.Entry(group.School).State = EntityState.Unchanged;
    }
    public async Task AddGroupTeacher(GroupTeacher group)
    {
        try
        {
            context.GroupTeachers.AddAsync(group);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        
    }
}