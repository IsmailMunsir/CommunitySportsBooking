using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunitySportsBooking.Services
{
    public interface IReviewService
    {
        Task<List<Review>> GetAllAsync();
        Task<List<Review>> GetByFacilityAsync(int facilityId);
        Task CreateAsync(Review review);
        Task<double> GetAverageRatingAsync(int facilityId);
    }

    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context;
        public ReviewService(ApplicationDbContext context) { _context = context; }

        public async Task<List<Review>> GetAllAsync() =>
            await _context.Reviews.Include(r => r.Member).Include(r => r.Facility)
                .OrderByDescending(r => r.CreatedAt).ToListAsync();

        public async Task<List<Review>> GetByFacilityAsync(int facilityId) =>
            await _context.Reviews.Include(r => r.Member).Include(r => r.Facility)
                .Where(r => r.FacilityId == facilityId)
                .OrderByDescending(r => r.CreatedAt).ToListAsync();

        public async Task CreateAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }

        public async Task<double> GetAverageRatingAsync(int facilityId)
        {
            var ratings = await _context.Reviews.Where(r => r.FacilityId == facilityId).Select(r => r.Rating).ToListAsync();
            return ratings.Count == 0 ? 0 : ratings.Average();
        }
    }
}