using MongoDB.Driver;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;

        public MongoUserRepository(IMongoDatabase database)
        {
            _usersCollection = database.GetCollection<User>("Users");
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _usersCollection
                .Find(v => v.Id.Equals(id))
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _usersCollection
                .Find(v => v.Username == username)
                .FirstOrDefaultAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            await _usersCollection.InsertOneAsync(user);
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            await _usersCollection
                .ReplaceOneAsync(v => v.Id == user.Id, user);
        }

        public async Task DeleteAsync(string id)
        {
            await _usersCollection.DeleteOneAsync(v => v.Id.Equals(id));
        }
    }
}
