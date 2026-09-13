using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunitySportsBooking.Models
{
    public class Facility
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required, MaxLength(50)]
        public string Type { get; set; } = string.Empty;

        [Required, MaxLength(150)]
        public string Location { get; set; } = string.Empty;

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int Capacity { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal PricePerHour { get; set; }

        [MaxLength(255)]
        public string? ImageUrl { get; set; }

        [MaxLength(5)]
        public string OpeningTime { get; set; } = "06:00";

        [MaxLength(5)]
        public string ClosingTime { get; set; } = "22:00";

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}