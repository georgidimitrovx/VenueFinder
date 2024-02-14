using MongoDB.Driver;
using VenueFinder.Application.Interfaces;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Application
{
    public class VenueService : IVenueService
    {
        private readonly IVenueRepository _venueRepository;
        private readonly ICoinmapService _coinmapService;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromHours(1);

        public VenueService(IVenueRepository venueRepository, ICoinmapService coinmapService)
        {
            _venueRepository = venueRepository;
            _coinmapService = coinmapService;
        }

        public async Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category)
        {
            var recentUpdateThreshold = DateTime.Now.Subtract(_cacheDuration);
            var cachedVenues = await _venueRepository
                .GetVenuesByCategoryAsync(category, recentUpdateThreshold);

            if (cachedVenues.Any())
                return cachedVenues;

            var externalVenues = await _coinmapService.GetVenuesByCategoryAsync(category);

            // Update cache
            foreach (var venue in externalVenues)
            {
                var venueCache = await _venueRepository.GetByIdAsync(venue.Id);
                if (venueCache != null)
                {
                    await UpdateVenueAsync(venue);
                }
                else
                {
                    await CreateVenueAsync(venue);
                }
            }

            return externalVenues;
        }

        public async Task<Venue> GetVenueByIdAsync(string id)
        {
            var recentUpdateThreshold = DateTime.Now.Subtract(_cacheDuration);
            var cachedVenue = await _venueRepository
                .GetByIdAsync(id);

            if (cachedVenue.LastUpdated > recentUpdateThreshold)
                return cachedVenue;

            var externalVenue = await _coinmapService.GetVenueAsync(id);

            // Update cache
            if (cachedVenue != null)
            {
                await UpdateVenueAsync(externalVenue);
            }
            else
            {
                await CreateVenueAsync(externalVenue);
            }

            return externalVenue;
        }

        public async Task<Venue> CreateVenueAsync(Venue venue)
        {
            // Additional validation or preprocessing can be performed here
            return await _venueRepository.AddAsync(venue);
        }

        public async Task UpdateVenueAsync(Venue venue)
        {
            // Ensure the venue's ID matches the provided ID, or perform other security checks
            await _venueRepository.UpdateAsync(venue);
        }

        public async Task DeleteVenueAsync(string id)
        {
            await _venueRepository.DeleteAsync(id);
        }
    }
}
