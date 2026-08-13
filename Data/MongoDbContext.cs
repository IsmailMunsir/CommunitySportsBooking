using CommunitySportsBooking.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CommunitySportsBooking.Data
{
    /// <summary>
    /// Central access point for all MongoDB collections used by the application.
    /// Registered as a singleton in Program.cs (MongoClient is thread-safe and
    /// designed to be reused across the lifetime of the app).
    /// </summary>
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _settings;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
        }

        public IMongoCollection<Member> Members =>
            _database.GetCollection<Member>(_settings.MembersCollection);

        public IMongoCollection<Facility> Facilities =>
            _database.GetCollection<Facility>(_settings.FacilitiesCollection);

        public IMongoCollection<Booking> Bookings =>
            _database.GetCollection<Booking>(_settings.BookingsCollection);

        public IMongoCollection<Review> Reviews =>
            _database.GetCollection<Review>(_settings.ReviewsCollection);

        public IMongoCollection<Inquiry> Inquiries =>
            _database.GetCollection<Inquiry>(_settings.InquiriesCollection);
    }
}