using MongoDB.Driver;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Infrastructure.Repositories
{
    public class MongoVenueRepository : IVenueRepository
    {
        private readonly IMongoCollection<Venue> _venuesCollection;

        public MongoVenueRepository(IMongoDatabase database)
        {
            _venuesCollection = database.GetCollection<Venue>("Venues");
        }

        public async Task<IEnumerable<Venue>> GetVenuesByCategoryAsync(string category,
            DateTime lastUpdated)
        {
            return await _venuesCollection
                .Find(v => v.Category == category && v.LastUpdated >= lastUpdated)
                .ToListAsync();
        }

        public async Task<Venue> GetByIdAsync(string id)
        {
            return await _venuesCollection.Find(v => v.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<Venue> AddAsync(Venue venue)
        {
            await _venuesCollection.InsertOneAsync(venue);
            return venue;
        }

        public async Task UpdateAsync(Venue venue)
        {
            await _venuesCollection.ReplaceOneAsync(v => v.Id == venue.Id, venue);
        }

        public async Task DeleteAsync(string id)
        {
            await _venuesCollection.DeleteOneAsync(v => v.Id.Equals(id));
        }
    }
}
