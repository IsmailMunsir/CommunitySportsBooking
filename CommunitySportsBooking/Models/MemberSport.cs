using System.ComponentModel.DataAnnotations;

namespace CommunitySportsBooking.Models
{
    // Resolves Member's multi-valued PreferredSports attribute into 1NF (junction table)
    public class MemberSport
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        [Required, MaxLength(50)]
        public string SportName { get; set; } = string.Empty;
    }
}