using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using MongoDB.Driver;

namespace CommunitySportsBooking.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetByMemberAsync(string memberId);
        Task<bool> IsSlotAvailableAsync(string facilityId, DateTime date, string startTime, string endTime);
        Task CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(string id);
        Task UpdateStatusAsync(string id, BookingStatus status);
    }

    public class BookingService : IBookingService
    {
        private readonly MongoDbContext _context;

        public BookingService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetByMemberAsync(string memberId)
        {
            return await _context.Bookings
                .Find(b => b.MemberId == memberId)
                .SortByDescending(b => b.BookingDate)
                .ToListAsync();
        }

        public async Task<bool> IsSlotAvailableAsync(string facilityId, DateTime date, string startTime, string endTime)
        {
            var filter = Builders<Booking>.Filter.Where(b =>
                b.FacilityId == facilityId &&
                b.BookingDate.Date == date.Date &&
                b.Status != BookingStatus.Cancelled);

            var existingBookings = await _context.Bookings.Find(filter).ToListAsync();

            foreach (var existing in existingBookings)
            {
                if (string.Compare(startTime, existing.EndTime, StringComparison.Ordinal) < 0 &&
                    string.Compare(existing.StartTime, endTime, StringComparison.Ordinal) < 0)
                {
                    return false; // overlap found
                }
            }

            return true;
        }

        public async Task CreateAsync(Booking booking)
        {
            await _context.Bookings.InsertOneAsync(booking);
        }

        public async Task<Booking?> GetByIdAsync(string id)
        {
            return await _context.Bookings.Find(b => b.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateStatusAsync(string id, BookingStatus status)
        {
            var update = Builders<Booking>.Update.Set(b => b.Status, status);
            await _context.Bookings.UpdateOneAsync(b => b.Id == id, update);
        }
    }
}