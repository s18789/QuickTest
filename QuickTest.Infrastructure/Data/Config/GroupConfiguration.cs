using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Data.Config;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(100);

        builder.HasMany(g => g.GroupTeachers)
           .WithOne(gt => gt.Group)
           .HasForeignKey(gt => gt.GroupId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
