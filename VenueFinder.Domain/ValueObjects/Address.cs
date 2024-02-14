using MongoDB.Bson.Serialization.Attributes;

namespace VenueFinder.Domain.ValueObjects
{
    /// <summary>
    /// Address Value Object
    /// Encapsulate the address details of a venue.
    /// </summary>
    public class Address
    {
        [BsonElement("street")]
        public string Street { get; }

        [BsonElement("city")]
        public string City { get; }

        [BsonElement("state")]
        public string State { get; }

        [BsonElement("postcode")]
        public string PostalCode { get; }

        [BsonElement("country")]
        public string Country { get; }

        public Address()
        {
            Street = string.Empty;
            City = string.Empty;
            State = string.Empty;
            PostalCode = string.Empty;
            Country = string.Empty;
        }

        public Address(string street, string city, string state, string postalCode, string country)
        {
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
            Country = country;
        }


        // Override Equals, GetHashCode, and ToString methods
    }
}
