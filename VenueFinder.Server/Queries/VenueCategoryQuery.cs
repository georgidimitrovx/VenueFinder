using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Queries
{
    public class VenueCategoryQuery
    {
        public async Task<IEnumerable<VenueCategory>> GetAllAsync(
            [Service] IVenueCategoryService venueCategoryService) =>
            await venueCategoryService.GetAllAsync();
    }
}
