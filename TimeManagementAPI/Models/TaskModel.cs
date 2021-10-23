using System;

namespace TimeManagementAPI.Models
{
    public class TaskModel : BaseModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime DateTime { get; set; }

        public string Status { get; set; }
    }
}
