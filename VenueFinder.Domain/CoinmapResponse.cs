using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain
{
    public class CoinmapResponse
    {
        public List<Venue> Venues { get; set; } = new List<Venue>();
    }
}
