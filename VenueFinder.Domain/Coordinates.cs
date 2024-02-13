namespace VenueFinder.Domain
{
    /// <summary>
    /// Coordinates Value Object
    /// Stores the geographical location of the venue.
    /// </summary>
    public class Coordinates
    {
        public double Latitude { get; }
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
