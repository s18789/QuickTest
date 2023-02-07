using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;
public class ExamConfiguration : IEntityTypeConfiguration<Exam>
{
    public void Configure(EntityTypeBuilder<Exam> builder)
    {
        builder.Property(x => x.Title).HasMaxLength(100);

        builder.HasOne(o => o.Teacher)
            .WithMany(m => m.Exams)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
