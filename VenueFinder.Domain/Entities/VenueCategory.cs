using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.Entities
{
    /// <summary>
    /// Venue Category Entity
    /// Represents the category of the venue
    /// </summary>
    public class VenueCategory
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("updated_on")]
        public DateTime LastUpdated { get; set; }

        // Constructor and methods
        public VenueCategory()
        {
            Id = string.Empty;
            Name = string.Empty;
            LastUpdated = DateTime.MinValue;
        }

        public VenueCategory(string id, string name, DateTime lastUpdated)
        {
            Id = id;
            Name = name;
            LastUpdated = lastUpdated;
        }
    }
}
