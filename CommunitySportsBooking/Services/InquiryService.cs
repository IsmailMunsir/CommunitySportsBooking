using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;

namespace CommunitySportsBooking.Services
{
    public interface IInquiryService
    {
        Task CreateAsync(Inquiry inquiry);
    }

    public class InquiryService : IInquiryService
    {
        private readonly ApplicationDbContext _context;
        public InquiryService(ApplicationDbContext context) { _context = context; }

        public async Task CreateAsync(Inquiry inquiry)
        {
            _context.Inquiries.Add(inquiry);
            await _context.SaveChangesAsync();
        }
    }
}