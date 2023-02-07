using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;
public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(x => x.QuestionContent).HasMaxLength(4000);

        /*builder.HasOne(o => o.Exam)
                .WithMany(m => m.Questions)
                .HasForeignKey(fk => fk.ExamId);*/
    }
}
