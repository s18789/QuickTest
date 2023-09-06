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
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext dbContext) : base(dbContext)
        {
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
        }
    }
}
