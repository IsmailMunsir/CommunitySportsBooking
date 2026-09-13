using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using MongoDB.Driver;

namespace CommunitySportsBooking.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetByFacilityAsync(string facilityId);
        Task CreateAsync(Review review);
        Task<double> GetAverageRatingAsync(string facilityId);
    }

    public class ReviewService : IReviewService
    {
        private readonly MongoDbContext _context;

        public ReviewService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews.Find(_ => true)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Review>> GetByFacilityAsync(string facilityId)
        {
            return await _context.Reviews
                .Find(r => r.FacilityId == facilityId)
                .SortByDescending(r => r.CreatedAt)
                .ToListAsync();
        }

        public async Task CreateAsync(Review review)
        {
            await _context.Reviews.InsertOneAsync(review);
        }

        public async Task<double> GetAverageRatingAsync(string facilityId)
        {
            var reviews = await GetByFacilityAsync(facilityId);
            return reviews.Count == 0 ? 0 : reviews.Average(r => r.Rating);
        }
    }
}