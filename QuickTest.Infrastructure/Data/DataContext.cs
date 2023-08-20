using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using System.Reflection;

namespace QuickTest.Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Admin> Admins { get; set; }
    
    public DbSet<Exam> Exams { get; set; }

    public DbSet<ExamResult> ExamResults { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<School> Schools { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<PredefinedAnswer> PredefinedAnswers { get; set; }

    public DbSet<Group> Groups { get; set; }

    public DbSet<StudentAnswer> StudentAnswers { get; set; }

    public DbSet<SelectedStudentAnswer> SelectedStudentAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
