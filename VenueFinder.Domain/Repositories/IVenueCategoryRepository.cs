using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain.Repositories
{
    public interface IVenueCategoryRepository
    {
        Task<IEnumerable<VenueCategory>> GetAllAsync(DateTime lastUpdated);
        Task<VenueCategory> GetByNameAsync(string name);
        Task<VenueCategory> AddAsync(VenueCategory venueCategory);
        Task UpdateAsync(VenueCategory venueCategory);
        Task DeleteAsync(string name);
    }
}
