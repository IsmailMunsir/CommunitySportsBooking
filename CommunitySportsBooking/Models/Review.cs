using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportsBooking.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public int? BookingId { get; set; }
        public Booking? Booking { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required, MaxLength(500)]
        public string Comment { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public string MemberName => Member?.FullName ?? string.Empty;

        [NotMapped]
        public string FacilityName => Facility?.Name ?? string.Empty;
    }
}