using VenueFinder.Domain.Entities;

namespace VenueFinder.Domain.Repositories
{
    public interface IVenueRepository
    {
        Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category, DateTime lastUpdated);
        Task<Venue> GetByIdAsync(string id);
        Task<Venue> AddAsync(Venue venue);
        Task UpdateAsync(Venue venue);
        Task DeleteAsync(string id);
    }
}
