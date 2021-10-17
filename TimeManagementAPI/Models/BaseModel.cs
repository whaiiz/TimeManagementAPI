using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TimeManagementAPI.Models
{
    public class BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual string Id { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
