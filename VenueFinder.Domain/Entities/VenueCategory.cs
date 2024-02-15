using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;

namespace VenueFinder.Domain.Entities
{
    /// <summary>
    /// Venue Category Entity
    /// Represents the category of the venue
    /// </summary>
    public class VenueCategory
    {
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("last_updated")]
        public DateTime LastUpdated { get; set; }

        // Constructor and methods
        public VenueCategory()
        {
            Name = string.Empty;
            LastUpdated = DateTime.MinValue;
        }

        public VenueCategory(string name, DateTime lastUpdated)
        {
            Name = name;
            LastUpdated = lastUpdated;
        }
    }
}
