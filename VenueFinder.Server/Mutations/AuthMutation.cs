using VenueFinder.Application.Interfaces;
using VenueFinder.Domain;
using VenueFinder.Domain.Entities;

namespace VenueFinder.API.Mutations
{
    public class AuthMutation
    {
        public async Task<AuthPayload> SignInAsync(string username, string password,
            [Service] IUserService userService,
            [Service] IAuthService authService)
        {
            var user = await userService.GetByUsernameAsync(username);
            if (user == null)
            {
                throw new GraphQLException("User does not exist.");
            }

            if (!authService.VerifyPassword(password, user.PasswordHash))
            {
                throw new GraphQLException("Authentication failed. Incorrect password.");
            }

            var token = authService.GenerateJwtToken(username);

            return new AuthPayload(username, token);
        }

        public async Task<AuthPayload> SignUpAsync(string username, string password,
            [Service] IAuthService authService,
            [Service] IUserService userService)
        {

            var user = await userService.GetByUsernameAsync(username);
            if (user != null)
            {
                throw new GraphQLException("User already exists.");
            }

            user = new User();
            user.Username = username;
            user.PasswordHash = authService.HashPassword(password);

            user = await userService.CreateAsync(user);
            if (user == null)
            {
                throw new GraphQLException("User creation failed.");
            }

            var token = authService.GenerateJwtToken(username);

            return new AuthPayload(username, token);
        }
    }
}
