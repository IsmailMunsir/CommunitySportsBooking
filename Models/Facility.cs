using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunitySportsBooking.Models
{
    /// <summary>
    /// A bookable community sports facility (tennis court, soccer field, basketball court, etc.)
    /// </summary>
    public class Facility
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("type")]
        public string Type { get; set; } = string.Empty; // Tennis Court, Soccer Field, Basketball Court...

        [BsonElement("location")]
        public string Location { get; set; } = string.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("capacity")]
        public int Capacity { get; set; }

        [BsonElement("pricePerHour")]
        public decimal PricePerHour { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        [BsonElement("openingTime")]
        public string OpeningTime { get; set; } = "06:00";

        [BsonElement("closingTime")]
        public string ClosingTime { get; set; } = "22:00";

        [BsonElement("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}