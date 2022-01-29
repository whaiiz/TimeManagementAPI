using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace TimeManagementAPI.Models
{
    public class TaskModel : BaseModel
    {
        [Required(ErrorMessage = "Please enter the task name.")]
        [MaxLength(32, ErrorMessage = "The max lenght of name is 32 characters.")]
        [BsonElement("name")]
        public string Name { get; set; }

        [MaxLength(200)]
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
