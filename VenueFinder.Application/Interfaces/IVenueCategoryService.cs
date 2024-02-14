using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Interfaces
{
    public interface IVenueCategoryService
    {
        Task<IEnumerable<VenueCategory>> GetAllAsync();
        Task<VenueCategory> GetByNameAsync(string name);
        Task<VenueCategory> CreateAsync(VenueCategory venueCategory);
        Task UpdateAsync(VenueCategory venueCategory);
        Task DeleteAsync(string name);
    }
}
