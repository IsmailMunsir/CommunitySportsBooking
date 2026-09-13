using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunitySportsBooking.Services
{
    public interface IBookingService
    {
        Task<List<Booking>> GetByMemberAsync(int memberId);
        Task<bool> IsSlotAvailableAsync(int facilityId, DateTime date, string startTime, string endTime);
        Task CreateAsync(Booking booking);
        Task<Booking?> GetByIdAsync(int id);
        Task UpdateStatusAsync(int id, BookingStatus status);
    }

    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        public BookingService(ApplicationDbContext context) { _context = context; }

        public async Task<List<Booking>> GetByMemberAsync(int memberId) =>
            await _context.Bookings.Include(b => b.Facility)
                .Where(b => b.MemberId == memberId)
                .OrderByDescending(b => b.BookingDate)
                .ToListAsync();

        public async Task<bool> IsSlotAvailableAsync(int facilityId, DateTime date, string startTime, string endTime)
        {
            // Pull same-day bookings for this facility, then compare precisely in C#
            // (keeps the exact same overlap logic as before, translated safely).
            var sameDayBookings = await _context.Bookings
                .Where(b => b.FacilityId == facilityId && b.BookingDate.Date == date.Date && b.Status != BookingStatus.Cancelled)
                .ToListAsync();

            foreach (var existing in sameDayBookings)
            {
                if (string.Compare(startTime, existing.EndTime, StringComparison.Ordinal) < 0 &&
                    string.Compare(existing.StartTime, endTime, StringComparison.Ordinal) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task CreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id) =>
            await _context.Bookings.FirstOrDefaultAsync(b => b.Id == id);

        public async Task UpdateStatusAsync(int id, BookingStatus status)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                booking.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}