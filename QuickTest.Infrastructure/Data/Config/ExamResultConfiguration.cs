using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;
public class ExamResultConfiguration : IEntityTypeConfiguration<ExamResult>
{
    public void Configure(EntityTypeBuilder<ExamResult> builder)
    {
       // builder.Property(x => x.Description).HasMaxLength(4000);

       /* builder.HasOne(o => o.Exam)
                .WithMany(m => m.ExamResults)
                .HasForeignKey(fk => fk.ExamId);

        builder.HasOne(o => o.User)
            .WithMany(m => m.ExamResults)
            .HasForeignKey(fk => fk.UserId);*/
    }
}
