using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Interfaces
{
    public interface IVenueService
    {
        Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category, string limit, string offset);
        Task<Venue> GetVenueByIdAsync(string id);
        Task<Venue> CreateVenueAsync(Venue venue);
        Task UpdateVenueAsync(Venue venue);
        Task DeleteVenueAsync(string id);
    }
}
