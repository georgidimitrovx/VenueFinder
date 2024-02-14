using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.ValueObjects
{
    /// <summary>
    /// Coordinates Value Object
    /// Stores the geographical location of the venue.
    /// </summary>
    public class Coordinates
    {
        [BsonElement("latitude")]
        public double Latitude { get; }

        [BsonElement("longitude")]
        public double Longitude { get; }

        public Coordinates()
        {
        }

        public Coordinates(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }


        // Override Equals, GetHashCode, and ToString methods
    }
}
