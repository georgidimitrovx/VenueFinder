using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.Entities
{
    /// <summary>
    /// Venue Entity
    /// Represent a place or location that your application is concerned with.
    /// </summary>
    public class Venue
    {
        [BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("last_updated")]
        public DateTime LastUpdated { get; set; }

        // Constructor and methods
        public Venue()
        {
            Id = string.Empty;
            Name = string.Empty;
            Category = string.Empty;
            LastUpdated = DateTime.MinValue;
        }

        public Venue(string id, string name, string category, DateTime lastUpdated)
        {
            Id = id;
            Name = name;
            Category = category;
            LastUpdated = lastUpdated;
        }
    }

}
