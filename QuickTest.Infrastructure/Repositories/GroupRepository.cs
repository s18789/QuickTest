using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Infrastructure.Repositories;
public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    private readonly string _connectionString;

    public GroupRepository(DataContext context) : base(context)
    {
        _connectionString = context.Database.GetDbConnection().ConnectionString;
    }
    private DataContext CreateNewContext()
    {
        return new DataContext(new DbContextOptionsBuilder<DataContext>().UseSqlServer(_connectionString).Options);
    }

    public async Task<bool> CheckIfGroupExists(string groupName)
    {
        using (var context = CreateNewContext())
        {
            return await context.Groups.AnyAsync(x => x.Name == groupName);
        }
    }
    public async Task<Group> GetGroupById(int id)
    {
        using (var context = CreateNewContext())
        {
            return await context.Groups.FindAsync(id);
        }
    }
    public async Task<Group> GetGroupByName(string groupName)
    {
        using (var context = CreateNewContext())
        {
            return await context.Groups.FirstOrDefaultAsync(x => x.Name == groupName);
        }
    }
    public async Task<Group> CreateGroupByName(string groupName, School school)
    {
        using (var context = CreateNewContext())
        {
            if (await CheckIfGroupExists(groupName))
            {
                return await GetGroupByName(groupName);
            }

            var groupToInsert = new Group
            {
                Name = groupName,
                School = school
            };

            try
            {
                if (context.Entry(school).State == EntityState.Detached)
                {
                    context.Schools.Attach(school);
                }
                await context.Groups.AddAsync(groupToInsert);
                var affectedRows = await context.SaveChangesAsync();
                if (affectedRows == 0)
                {
                    Console.WriteLine("Group was not created. No rows were affected.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Group was not created because an error occurred: " + ex.ToString());
            }

            return groupToInsert;
        }
    }

    public async Task DetachThatMfcker(Group group)
    {
        using (var context = CreateNewContext())
        {
            context.Entry(group.School).State = EntityState.Detached;
        }
    }
    public async Task DetachGroupTeacherMfcker(Group group)
    {
        using (var context = CreateNewContext())
        {
            foreach (var groupTeacher in group.GroupTeachers)
            {
                context.Entry(groupTeacher).State = EntityState.Detached;
            }
        }
    }

    public async Task AddGroupTeacher(GroupTeacher group)
    {
        
            try
            {
                await context.GroupTeachers.AddAsync(group);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        
    }
}