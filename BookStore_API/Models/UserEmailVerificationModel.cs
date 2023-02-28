using MongoDB.Bson.Serialization.Attributes;

namespace BookStore_API.Models
{
    public class UserEmailVerificationModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? UserId { get; set; }
        public Token? EmailVerificationToken { get; set; }
    }
}
