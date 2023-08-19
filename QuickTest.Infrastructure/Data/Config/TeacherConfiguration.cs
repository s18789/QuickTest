using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Data.Config
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            
            builder.HasMany(t => t.GroupTeachers)
                   .WithOne(gt => gt.Teacher)
                   .HasForeignKey(gt => gt.TeacherId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
