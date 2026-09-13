using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportsBooking.Models
{
    public enum BookingStatus { Pending, Confirmed, Cancelled, Completed }

    public class Booking
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public int FacilityId { get; set; }
        public Facility? Facility { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }

        [Required, MaxLength(5)]
        public string StartTime { get; set; } = string.Empty;

        [Required, MaxLength(5)]
        public string EndTime { get; set; } = string.Empty;

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        [MaxLength(300)]
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Review? Review { get; set; }

        // Convenience read-only properties so existing Views keep working unchanged
        // (no separate denormalised column — always reflects the live related row).
        [NotMapped]
        public string MemberName => Member?.FullName ?? string.Empty;

        [NotMapped]
        public string FacilityName => Facility?.Name ?? string.Empty;
    }
}