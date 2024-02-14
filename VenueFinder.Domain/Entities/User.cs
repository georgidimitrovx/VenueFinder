using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; }

        [BsonElement("fullName")]
        public string FullName { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }

        public User()
        {
            Id = string.Empty;
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            FullName = string.Empty;
            CreatedAt = DateTime.MinValue;
        }

        public User(string id, string username, string email, string passwordHash,
            string fullName, DateTime createdAt)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            CreatedAt = createdAt;
        }
    }
}
