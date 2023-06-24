using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace online_shopping_app.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; } = String.Empty;
        [BsonElement("firstName")]
        public string FirstName { get; set; } = String.Empty;
        [BsonElement("lastName")]
        public string LastName { get; set; } = String.Empty;
        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;
        [BsonElement("coontactNumber")]
        public string ContactNumber { get; set; } = String.Empty;
        [BsonElement("loginId")]
        public string LoginId { get; set; } = String.Empty;
        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;
        [BsonElement("role")]
        public string Role { get; set; } = String.Empty;

    }
}
