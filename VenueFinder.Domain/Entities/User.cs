using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VenueFinder.Domain.Entities
{
    public class User
    {
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

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
            Username = string.Empty;
            Email = string.Empty;
            PasswordHash = string.Empty;
            FullName = string.Empty;
            CreatedAt = DateTime.MinValue;
        }

        public User(string username, string email, string passwordHash,
            string fullName, DateTime createdAt)
        {
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            CreatedAt = createdAt;
        }
    }
}
