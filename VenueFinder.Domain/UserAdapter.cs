using VenueFinder.Domain.Dto;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain
{
    public static class UserAdapter
    {
        public static User Adapt(UserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                PasswordHash = dto.Password,
            };

            return user;
        }
    }
}
