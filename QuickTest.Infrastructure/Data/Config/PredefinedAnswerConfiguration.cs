using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;

public class PredefinedAnswerConfiguration : IEntityTypeConfiguration<PredefinedAnswer>
{
    public void Configure(EntityTypeBuilder<PredefinedAnswer> builder)
    {
        builder.Property(x => x.Content).HasMaxLength(4000);

        /*builder.HasOne(o => o.Question)
            .WithMany(m => m.PredefinedAnswers)
            .HasForeignKey(fk => fk.QuestionId);*/
    }
}
