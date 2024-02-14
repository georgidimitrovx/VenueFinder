using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Application
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            // Additional validation or preprocessing can be performed here
            return await _userRepository.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            // Ensure the user's ID matches the provided ID, or perform other security checks
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
