using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TimeManagementAPI.Models
{
    public class Task
    {
        [BsonId]
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime DateTime { get; set; }

        public DateTime CreatedAt { get; set; }

        public TaskStatus TaskStatus { get; set; }
    }
}
