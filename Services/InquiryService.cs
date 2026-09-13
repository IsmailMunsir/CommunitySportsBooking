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
        private readonly MongoDbContext _context;

        public InquiryService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Inquiry inquiry)
        {
            await _context.Inquiries.InsertOneAsync(inquiry);
        }
    }
}