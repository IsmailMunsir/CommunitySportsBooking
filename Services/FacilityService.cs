using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using MongoDB.Driver;

namespace CommunitySportsBooking.Services
{
    public interface IFacilityService
    {
        Task<List<Facility>> GetAllAsync();
        Task<Facility?> GetByIdAsync(string id);
        Task<List<Facility>> SearchAsync(string? type, string? location);
    }

    public class FacilityService : IFacilityService
    {
        private readonly MongoDbContext _context;

        public FacilityService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Facility>> GetAllAsync()
        {
            return await _context.Facilities.Find(f => f.IsActive).ToListAsync();
        }

        public async Task<Facility?> GetByIdAsync(string id)
        {
            return await _context.Facilities.Find(f => f.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Facility>> SearchAsync(string? type, string? location)
        {
            var filterBuilder = Builders<Facility>.Filter;
            var filter = filterBuilder.Eq(f => f.IsActive, true);

            if (!string.IsNullOrWhiteSpace(type))
            {
                filter &= filterBuilder.Eq(f => f.Type, type);
            }

            if (!string.IsNullOrWhiteSpace(location))
            {
                filter &= filterBuilder.Regex(f => f.Location,
                    new MongoDB.Bson.BsonRegularExpression(location, "i"));
            }

            return await _context.Facilities.Find(filter).ToListAsync();
        }
    }
}