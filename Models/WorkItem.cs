using System;

namespace TimeManagementAPI.Models
{
    public class WorkItem : BaseModel
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime DateTime { get; set; }

        public WorkItemStatus WorkItemStatus { get; set; }
    }
}
