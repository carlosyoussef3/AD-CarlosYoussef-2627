using TimesheetApi.Model;

namespace TimesheetApi.Data
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddAsync(User user);
    }
}
