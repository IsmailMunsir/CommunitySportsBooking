using CommunitySportsBooking.Models;
using CommunitySportsBooking.Services;
using MongoDB.Driver;

namespace CommunitySportsBooking.Data
{
    public static class SeedData
    {
        public static void Initialize(MongoDbContext context)
        {
            SeedFacilities(context);
            SeedDemoMember(context);
        }

        private static void SeedFacilities(MongoDbContext context)
        {
            if (context.Facilities.CountDocuments(FilterDefinition<Facility>.Empty) > 0)
                return;

            var facilities = new List<Facility>
            {
                new Facility
                {
                    Name = "Riverside Tennis Court 1",
                    Type = "Tennis Court",
                    Location = "Riverside Sports Complex",
                    Description = "Hard-court tennis facility with floodlights, suitable for evening play.",
                    Capacity = 4,
                    PricePerHour = 12.00m,
                    ImageUrl = "/img/tennis.jpg",
                    OpeningTime = "06:00",
                    ClosingTime = "22:00"
                },
                new Facility
                {
                    Name = "Central Park Soccer Field A",
                    Type = "Soccer Field",
                    Location = "Central Park Grounds",
                    Description = "Full-size grass soccer pitch with changing rooms.",
                    Capacity = 22,
                    PricePerHour = 40.00m,
                    ImageUrl = "/img/soccer.jpg",
                    OpeningTime = "07:00",
                    ClosingTime = "21:00"
                },
                new Facility
                {
                    Name = "Downtown Basketball Court",
                    Type = "Basketball Court",
                    Location = "Downtown Recreation Centre",
                    Description = "Indoor hardwood court with adjustable hoops, ideal for 5-a-side games.",
                    Capacity = 10,
                    PricePerHour = 18.00m,
                    ImageUrl = "/img/basketball.jpg",
                    OpeningTime = "08:00",
                    ClosingTime = "23:00"
                },
                new Facility
                {
                    Name = "Greenfield Badminton Hall",
                    Type = "Badminton Court",
                    Location = "Greenfield Sports Hall",
                    Description = "Air-conditioned indoor badminton hall with 4 courts.",
                    Capacity = 4,
                    PricePerHour = 10.00m,
                    ImageUrl = "/img/badminton.jpg",
                    OpeningTime = "06:00",
                    ClosingTime = "22:00"
                },
                new Facility
                {
                    Name = "Community Swimming Pool",
                    Type = "Swimming Pool",
                    Location = "Municipal Aquatic Centre",
                    Description = "25m heated pool with lane lines, open for lap swimming bookings.",
                    Capacity = 30,
                    PricePerHour = 15.00m,
                    ImageUrl = "/img/pool.jpg",
                    OpeningTime = "06:00",
                    ClosingTime = "20:00"
                },
                new Facility
                {
                    Name = "Eastside Volleyball Court",
                    Type = "Volleyball Court",
                    Location = "Eastside Recreation Park",
                    Description = "Outdoor sand volleyball court, popular for weekend leagues.",
                    Capacity = 12,
                    PricePerHour = 14.00m,
                    ImageUrl = "/img/volleyball.jpg",
                    OpeningTime = "07:00",
                    ClosingTime = "21:00"
                }
            };

            context.Facilities.InsertMany(facilities);
        }

        private static void SeedDemoMember(MongoDbContext context)
        {
            var existing = context.Members.Find(m => m.Email == "demo@sportsbooking.local").FirstOrDefault();
            if (existing != null) return;

            var (hash, salt) = PasswordHasher.HashPassword("Demo@123");

            var demoMember = new Member
            {
                FullName = "Demo Member",
                Email = "demo@sportsbooking.local",
                Phone = "0770000000",
                Address = "123 Community Lane",
                PreferredSports = new List<string> { "Tennis", "Soccer" },
                PasswordHash = hash,
                PasswordSalt = salt,
                IsQuickSignup = false
            };

            context.Members.InsertOne(demoMember);
        }
    }
}