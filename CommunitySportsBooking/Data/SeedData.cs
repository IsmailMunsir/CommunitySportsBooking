using CommunitySportsBooking.Models;
using CommunitySportsBooking.Services;

namespace CommunitySportsBooking.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            SeedFacilities(context);
            SeedDemoMember(context);
        }

        private static void SeedFacilities(ApplicationDbContext context)
        {
            if (context.Facilities.Any()) return;

            context.Facilities.AddRange(
                new Facility { Name = "Riverside Tennis Court 1", Type = "Tennis Court", Location = "Riverside Sports Complex",
                    Description = "Hard-court tennis facility with floodlights.", Capacity = 4, PricePerHour = 12.00m,
                    OpeningTime = "06:00", ClosingTime = "22:00" },
                new Facility { Name = "Central Park Soccer Field A", Type = "Soccer Field", Location = "Central Park Grounds",
                    Description = "Full-size grass soccer pitch with changing rooms.", Capacity = 22, PricePerHour = 40.00m,
                    OpeningTime = "07:00", ClosingTime = "21:00" },
                new Facility { Name = "Downtown Basketball Court", Type = "Basketball Court", Location = "Downtown Recreation Centre",
                    Description = "Indoor hardwood court with adjustable hoops.", Capacity = 10, PricePerHour = 18.00m,
                    OpeningTime = "08:00", ClosingTime = "23:00" },
                new Facility { Name = "Greenfield Badminton Hall", Type = "Badminton Court", Location = "Greenfield Sports Hall",
                    Description = "Air-conditioned indoor badminton hall with 4 courts.", Capacity = 4, PricePerHour = 10.00m,
                    OpeningTime = "06:00", ClosingTime = "22:00" },
                new Facility { Name = "Community Swimming Pool", Type = "Swimming Pool", Location = "Municipal Aquatic Centre",
                    Description = "25m heated pool with lane lines.", Capacity = 30, PricePerHour = 15.00m,
                    OpeningTime = "06:00", ClosingTime = "20:00" },
                new Facility { Name = "Eastside Volleyball Court", Type = "Volleyball Court", Location = "Eastside Recreation Park",
                    Description = "Outdoor sand volleyball court.", Capacity = 12, PricePerHour = 14.00m,
                    OpeningTime = "07:00", ClosingTime = "21:00" }
            );
            context.SaveChanges();
        }

        private static void SeedDemoMember(ApplicationDbContext context)
        {
            if (context.Members.Any(m => m.Email == "demo@sportsbooking.local")) return;

            var (hash, salt) = PasswordHasher.HashPassword("Demo@123");

            var demo = new Member
            {
                FullName = "Demo Member",
                Email = "demo@sportsbooking.local",
                Phone = "0770000000",
                Address = "123 Community Lane",
                PasswordHash = hash,
                PasswordSalt = salt,
                IsQuickSignup = false
            };
            demo.PreferredSports.Add(new MemberSport { SportName = "Tennis" });
            demo.PreferredSports.Add(new MemberSport { SportName = "Soccer" });

            context.Members.Add(demo);
            context.SaveChanges();
        }
    }
}