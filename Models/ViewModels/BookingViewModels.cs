using System.ComponentModel.DataAnnotations;

namespace CommunitySportsBooking.Models.ViewModels
{
    public class FacilitySearchViewModel
    {
        public string? FacilityType { get; set; }
        public string? Location { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }

        public List<Facility> Results { get; set; } = new();
        public bool IsRestricted { get; set; } = false; // true for guest restricted search

        public static readonly string[] FacilityTypes =
        {
            "Tennis Court", "Soccer Field", "Basketball Court", "Badminton Court", "Volleyball Court", "Swimming Pool", "Cricket Ground", "Athletics Track"
        };
    }

    public class CreateBookingViewModel
    {
        [Required]
        public string FacilityId { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;
        public decimal PricePerHour { get; set; }

        [Required(ErrorMessage = "Please choose a date")]
        [DataType(DataType.Date)]
        public DateTime BookingDate { get; set; } = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Please choose a start time")]
        public string StartTime { get; set; } = "10:00";

        [Required(ErrorMessage = "Please choose an end time")]
        public string EndTime { get; set; } = "11:00";

        [StringLength(300)]
        public string? Notes { get; set; }
    }

    public class CreateReviewViewModel
    {
        [Required]
        public string FacilityId { get; set; } = string.Empty;
        public string FacilityName { get; set; } = string.Empty;

        public string? BookingId { get; set; }

        [Required(ErrorMessage = "Please give a rating")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5")]
        public int Rating { get; set; } = 5;

        [Required(ErrorMessage = "Please write a short comment")]
        [StringLength(500, MinimumLength = 5)]
        public string Comment { get; set; } = string.Empty;
    }

    public class InquiryViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message is required")]
        [StringLength(1000, MinimumLength = 10)]
        public string Message { get; set; } = string.Empty;
    }
}