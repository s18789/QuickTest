using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Data.Config
{
    public class GroupTeacherConfiguration : IEntityTypeConfiguration<GroupTeacher>
    {
        public void Configure(EntityTypeBuilder<GroupTeacher> builder)
        {
            builder.HasKey(gt => new { gt.GroupId, gt.TeacherId });
        }
    }
}
