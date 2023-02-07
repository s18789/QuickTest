using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasOne(e => e.Teacher)
            .WithMany(m => m.Groups)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
