using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TimeManagementAPI.Models
{
    public class TaskModel : BaseModel
    {
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("dateTime")]
        public DateTime DateTime { get; set; }

        [BsonElement("status")]
        public string Status { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }
    }
}
