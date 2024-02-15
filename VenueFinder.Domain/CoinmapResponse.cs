using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain
{
    public class ListCoinmapResponse
    {
        public List<Venue> Venues { get; set; } = new List<Venue>();
    }

    public class CoinmapResponse
    {
        public required Venue Venue { get; set; }
    }
}
