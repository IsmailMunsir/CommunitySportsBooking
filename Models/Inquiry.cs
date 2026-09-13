using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CommunitySportsBooking.Models
{
    /// <summary>
    /// A message/inquiry sent by a guest through the public contact form.
    /// </summary>
    public class Inquiry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("subject")]
        public string Subject { get; set; } = string.Empty;

        [BsonElement("message")]
        public string Message { get; set; } = string.Empty;

        [BsonElement("isResolved")]
        public bool IsResolved { get; set; } = false;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}