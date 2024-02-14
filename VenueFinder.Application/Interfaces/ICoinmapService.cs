using VenueFinder.Domain.Entities;

namespace VenueFinder.Application.Interfaces
{
    public interface ICoinmapService
    {
        public Task<Venue> GetVenueAsync(string id);
        public Task<List<Venue>> GetAllVenuesAsync();
        public Task<List<Venue>> GetVenuesByCategoryAsync(string category);
    }
}
