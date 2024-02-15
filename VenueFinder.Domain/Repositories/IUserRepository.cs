using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}
