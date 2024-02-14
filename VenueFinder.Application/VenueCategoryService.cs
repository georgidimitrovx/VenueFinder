using MongoDB.Driver;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Application
{
    public class VenueCategoryService : IVenueCategoryService
    {
        private readonly IVenueCategoryRepository _venueCategoryRepository;
        private readonly ICoinmapService _coinmapService;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromDays(1);

        public VenueCategoryService(IVenueCategoryRepository venueRepository,
            ICoinmapService coinmapService)
        {
            _venueCategoryRepository = venueRepository;
            _coinmapService = coinmapService;
        }

        public async Task<IEnumerable<VenueCategory>> GetAllAsync()
        {
            var recentUpdateThreshold = DateTime.Now.Subtract(_cacheDuration);
            var cachedVenues = await _venueCategoryRepository.GetAllAsync(recentUpdateThreshold);

            if (cachedVenues.Any())
                return cachedVenues;

            var externalVenues = await _coinmapService.GetAllVenuesAsync();
            var categories = externalVenues.Select(v => v.Category).Distinct().ToList();

            // Update cache
            foreach (var category in categories)
            {
                var venueCategory = await _venueCategoryRepository.GetByNameAsync(category);
                if (venueCategory != null)
                {
                    venueCategory.LastUpdated = DateTime.Now;
                    await UpdateAsync(venueCategory);
                }
                else
                {
                    venueCategory = new VenueCategory("", category, DateTime.Now);
                    await CreateAsync(venueCategory);
                }
            }

            // Read fresh cached records
            cachedVenues = await _venueCategoryRepository.GetAllAsync(recentUpdateThreshold);

            return cachedVenues;
        }

        public async Task<VenueCategory> GetByNameAsync(string id)
        {
            return await _venueCategoryRepository.GetByNameAsync(id);
        }

        public async Task<VenueCategory> CreateAsync(VenueCategory venueCategory)
        {
            // Additional validation or preprocessing can be performed here
            return await _venueCategoryRepository.AddAsync(venueCategory);
        }

        public async Task UpdateAsync(VenueCategory venueCategory)
        {
            // Ensure the venue's ID matches the provided ID, or perform other security checks
            await _venueCategoryRepository.UpdateAsync(venueCategory);
        }

        public async Task DeleteAsync(string name)
        {
            await _venueCategoryRepository.DeleteAsync(name);
        }
    }
}
