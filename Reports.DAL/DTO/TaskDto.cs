using System;

namespace Reports.DAL.DTO
{
    public class TaskDto
    {
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
    }
}