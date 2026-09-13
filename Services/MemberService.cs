using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using MongoDB.Driver;

namespace CommunitySportsBooking.Services
{
    public interface IMemberService
    {
        Task<Member?> GetByEmailAsync(string email);
        Task<Member?> GetByIdAsync(string id);
        Task<bool> EmailExistsAsync(string email);
        Task CreateAsync(Member member);
    }

    public class MemberService : IMemberService
    {
        private readonly MongoDbContext _context;

        public MemberService(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _context.Members
                .Find(m => m.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();
        }

        public async Task<Member?> GetByIdAsync(string id)
        {
            return await _context.Members.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            var count = await _context.Members.CountDocumentsAsync(m => m.Email.ToLower() == email.ToLower());
            return count > 0;
        }

        public async Task CreateAsync(Member member)
        {
            await _context.Members.InsertOneAsync(member);
        }
    }
}