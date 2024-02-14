using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Queries
{
    public class VenueQuery
    {
        public async Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(
            [Service] IVenueService venueService, string category) =>
            await venueService.GetVenuesByCategoryAsync(category);
    }
}
