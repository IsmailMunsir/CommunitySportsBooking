using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunitySportsBooking.Models
{
    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled,
        Completed
    }

    /// <summary>
    /// A member's request to use a facility on a given date/time slot.
    /// </summary>
    public class Booking
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("memberId")]
        public string MemberId { get; set; } = string.Empty;

        [BsonElement("memberName")]
        public string MemberName { get; set; } = string.Empty;

        [BsonElement("facilityId")]
        public string FacilityId { get; set; } = string.Empty;

        [BsonElement("facilityName")]
        public string FacilityName { get; set; } = string.Empty;

        [BsonElement("bookingDate")]
        public DateTime BookingDate { get; set; }

        [BsonElement("startTime")]
        public string StartTime { get; set; } = string.Empty; // "14:00"

        [BsonElement("endTime")]
        public string EndTime { get; set; } = string.Empty;   // "15:00"

        [BsonElement("status")]
        [BsonRepresentation(BsonType.String)]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [BsonElement("notes")]
        public string Notes { get; set; } = string.Empty;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}