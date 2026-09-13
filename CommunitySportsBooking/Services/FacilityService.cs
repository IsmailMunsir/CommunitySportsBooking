using CommunitySportsBooking.Data;
using CommunitySportsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunitySportsBooking.Services
{
    public interface IFacilityService
    {
        Task<List<Facility>> GetAllAsync();
        Task<Facility?> GetByIdAsync(int id);
        Task<List<Facility>> SearchAsync(string? type, string? location);
    }

    public class FacilityService : IFacilityService
    {
        private readonly ApplicationDbContext _context;
        public FacilityService(ApplicationDbContext context) { _context = context; }

        public async Task<List<Facility>> GetAllAsync() =>
            await _context.Facilities.Where(f => f.IsActive).ToListAsync();

        public async Task<Facility?> GetByIdAsync(int id) =>
            await _context.Facilities.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<Facility>> SearchAsync(string? type, string? location)
        {
            var query = _context.Facilities.Where(f => f.IsActive);
            if (!string.IsNullOrWhiteSpace(type)) query = query.Where(f => f.Type == type);
            if (!string.IsNullOrWhiteSpace(location)) query = query.Where(f => f.Location.Contains(location));
            return await query.ToListAsync();
        }
    }
}