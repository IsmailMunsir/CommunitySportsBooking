using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunitySportsBooking.Services
{
    public interface IMemberService
    {
        Task<Member?> GetByEmailAsync(string email);
        Task<Member?> GetByIdAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task CreateAsync(Member member);
    }

    public class MemberService : IMemberService
    {
        private readonly ApplicationDbContext _context;
        public MemberService(ApplicationDbContext context) { _context = context; }

        public async Task<Member?> GetByEmailAsync(string email) =>
            await _context.Members.Include(m => m.PreferredSports)
                .FirstOrDefaultAsync(m => m.Email.ToLower() == email.ToLower());

        public async Task<Member?> GetByIdAsync(int id) =>
            await _context.Members.Include(m => m.PreferredSports).FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> EmailExistsAsync(string email) =>
            await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower());

        public async Task CreateAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
        }
    }
}