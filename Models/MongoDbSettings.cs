namespace CommunitySportsBooking.Models
{
    /// <summary>
    /// Strongly typed binding for the "MongoDbSettings" section in appsettings.json.
    /// </summary>
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string MembersCollection { get; set; } = string.Empty;
        public string FacilitiesCollection { get; set; } = string.Empty;
        public string BookingsCollection { get; set; } = string.Empty;
        public string ReviewsCollection { get; set; } = string.Empty;
        public string InquiriesCollection { get; set; } = string.Empty;
    }
}