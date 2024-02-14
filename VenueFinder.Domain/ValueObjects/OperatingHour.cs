using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.ValueObjects
{
    public class OperatingHour
    {
        [BsonElement("day")]
        public DayOfWeek Day { get; private set; }

        [BsonElement("openTime")]
        public TimeSpan OpenTime { get; private set; }

        [BsonElement("closeTime")]
        public TimeSpan CloseTime { get; private set; }

        // Constructor and methods
    }
}
