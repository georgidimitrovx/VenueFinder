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

        public async Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category,
            string limit, string offset)
        {
            var recentUpdateThreshold = DateTime.Now.Subtract(_cacheDuration);
            var cachedVenues = await _venueRepository
                .GetVenuesByCategoryAsync(category, limit, offset, recentUpdateThreshold);

            if (cachedVenues.Count() == int.Parse(limit))
                return cachedVenues;

            var externalVenues = await _coinmapService.GetVenuesByCategoryAsync(category, limit, offset);

            // Update cache
            foreach (var venue in externalVenues)
            {
                var venueCache = await _venueRepository.GetByIdAsync(venue.Id);
                if (venueCache != null)
                {
                    venue.LastUpdated = DateTime.UtcNow;
                    await UpdateVenueAsync(venue);
                }
                else
                {
                    venue.LastUpdated = DateTime.UtcNow;
                    await CreateVenueAsync(venue);
                }
            }

            return externalVenues;
        }

        public async Task<Venue> GetVenueByIdAsync(string id)
        {
            var recentUpdateThreshold = DateTime.UtcNow.Subtract(_cacheDuration);
            var cachedVenue = await _venueRepository.GetByIdAsync(id);

            if (cachedVenue != null && cachedVenue.LastUpdated > recentUpdateThreshold)
                return cachedVenue;

            var externalVenue = await _coinmapService.GetVenueAsync(id);

            // Update cache
            if (cachedVenue != null)
            {
                externalVenue.LastUpdated = DateTime.UtcNow;
                await UpdateVenueAsync(externalVenue);
            }
            else
            {
                externalVenue.LastUpdated = DateTime.UtcNow;
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
