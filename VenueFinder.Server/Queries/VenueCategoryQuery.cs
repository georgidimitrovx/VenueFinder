using HotChocolate.Authorization;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Queries
{
    [ExtendObjectType("Query")]
    [Authorize]
    public class VenueCategoryQuery
    {
        public async Task<IEnumerable<VenueCategory>> GetAllVenueCategoriesAsync(
            [Service] IVenueCategoryService venueCategoryService)
        {
            return await venueCategoryService.GetAllAsync();
        }

        public async Task<VenueCategory> GetVenueCategoryAsync(string name,
            [Service] IVenueCategoryService venueCategoryService)
        {
            return await venueCategoryService.GetByNameAsync(name);
        }
    }
}
