using Microsoft.EntityFrameworkCore;
using QuickTest.Core.Entities;
using QuickTest.Infrastructure.Data;
using QuickTest.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Infrastructure.Repositories
{
    public class SchoolRepository : BaseRepository<School>, ISchoolRepository
    {
        private readonly DataContext context;

        public SchoolRepository(DataContext dbContext) : base(dbContext)
        {
            this.context = dbContext;
        }

        public async Task<School> GetSchoolIncludeGroups(int id)
        {
            return await this.context.Schools
                .Where(s => s.Id == id)
                .Include(s => s.Groups)
                .FirstOrDefaultAsync();
        }

    }
}
