namespace VenueFinder.Domain
{
    /// <summary>
    /// Venue Entity
    /// Represent a place or location that your application is concerned with.
    /// </summary>
    public class Venue
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; private set; }
        public string Category { get; private set; }
        public Coordinates Coordinates { get; private set; }
        public string Description { get; private set; }
        public List<OperatingHour> OperatingHours { get; private set; }

        // Constructor and methods
        public Venue()
        {
            Name = string.Empty;
            Address = new Address();
            Category = string.Empty;
            Coordinates = new Coordinates();
            Description = string.Empty;
            OperatingHours = new List<OperatingHour>();
        }

        public Venue(Guid id, string name, Address address, string category,
            Coordinates coordinates, string description, List<OperatingHour> operatingHours)
        {
            Id = id;
            Name = name;
            Address = address;
            Category = category;
            Coordinates = coordinates;
            Description = description;
            OperatingHours = operatingHours;
        }
    }

}
