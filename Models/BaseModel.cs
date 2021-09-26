using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TimeManagementAPI.Models
{
    public class BaseModel
    {
        [BsonId]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
