using QuickTest.Core.Entities;

namespace QuickTest.Application.Services
{
    public interface IUserContextService
    {
        Task<User> GetUserContextAsync();
    }
}
