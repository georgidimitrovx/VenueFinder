namespace VenueFinder.Domain
{
    /// <summary>
    /// Address Value Object
    /// Encapsulate the address details of a venue.
    /// </summary>
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string PostalCode { get; }
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
