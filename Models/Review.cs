using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunitySportsBooking.Models
{
    /// <summary>
    /// A member's review of a facility after use. Visible to guests as well.
    /// </summary>
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("facilityId")]
        public string FacilityId { get; set; } = string.Empty;

        [BsonElement("facilityName")]
        public string FacilityName { get; set; } = string.Empty;

        [BsonElement("memberId")]
        public string MemberId { get; set; } = string.Empty;

        [BsonElement("memberName")]
        public string MemberName { get; set; } = string.Empty;

        [BsonElement("bookingId")]
        public string? BookingId { get; set; }

        [BsonElement("rating")]
        [BsonRepresentation(BsonType.Int32)]
        public int Rating { get; set; } // 1-5

        [BsonElement("comment")]
        public string Comment { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}