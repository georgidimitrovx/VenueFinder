using HotChocolate.Authorization;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class VenueQuery
    {
        public async Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category,
            string limit, string offset, [Service] IVenueService venueService)
        {
            return await venueService.GetVenuesByCategoryAsync(category, limit, offset);
        }

        public async Task<Venue> GetVenue(string id, [Service] IVenueService venueService)
        {
            return await venueService.GetVenueByIdAsync(id); ;
        }
    }
}
