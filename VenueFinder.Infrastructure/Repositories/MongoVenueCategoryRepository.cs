using MongoDB.Driver;
using VenueFinder.Domain.Entities;
using VenueFinder.Domain.Repositories;

namespace VenueFinder.Infrastructure.Repositories
{
    public class MongoVenueCategoryRepository : IVenueCategoryRepository
    {
        private readonly IMongoCollection<VenueCategory> _venueCategoriesCollection;

        public MongoVenueCategoryRepository(IMongoDatabase database)
        {
            _venueCategoriesCollection = database.GetCollection<VenueCategory>("VenueCategories");
        }

        public async Task<IEnumerable<VenueCategory>> GetAllAsync(DateTime lastUpdated)
        {
            return await _venueCategoriesCollection
                .Find(v => v.LastUpdated >= lastUpdated)
                .ToListAsync();
        }

        public async Task<VenueCategory> GetByNameAsync(string name)
        {
            return await _venueCategoriesCollection
                .Find(v => v.Name.Equals(name))
                .FirstOrDefaultAsync();
        }

        public async Task<VenueCategory> AddAsync(VenueCategory venueCategory)
        {
            await _venueCategoriesCollection.InsertOneAsync(venueCategory);
            return venueCategory;
        }

        public async Task UpdateAsync(VenueCategory venueCategory)
        {
            await _venueCategoriesCollection
                .ReplaceOneAsync(v => v.Id == venueCategory.Id, venueCategory);
        }

        public async Task DeleteAsync(string name)
        {
            await _venueCategoriesCollection.DeleteOneAsync(v => v.Name == name);
        }
    }
}