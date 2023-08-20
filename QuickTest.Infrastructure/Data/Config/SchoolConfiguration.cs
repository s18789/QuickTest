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
    public class SchoolConfiguration : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.HasKey(s => s.Id); 

            builder.HasOne(s => s.Address)
                   .WithOne(a => a.School)
                   .HasForeignKey<Address>(a => a.SchoolId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
