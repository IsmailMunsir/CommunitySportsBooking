using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunitySportsBooking.Models
{
    /// <summary>
    /// A registered member of the Community Sports Facilities Booking System.
    /// Members can sign in, search/book facilities and submit reviews.
    /// </summary>
    public class Member
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("fullName")]
        public string FullName { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("phone")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("address")]
        public string Address { get; set; } = string.Empty;

        [BsonElement("preferredSports")]
        public List<string> PreferredSports { get; set; } = new();

        [BsonElement("passwordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("passwordSalt")]
        public string PasswordSalt { get; set; } = string.Empty;

        /// <summary>
        /// True for members who signed up via the quick "Become a Member" guest flow
        /// (basic info only). False for full registrations with sports preferences etc.
        /// </summary>
        [BsonElement("isQuickSignup")]
        public bool IsQuickSignup { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}