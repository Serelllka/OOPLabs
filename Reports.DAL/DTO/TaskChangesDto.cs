using System;

namespace Reports.DAL.DTO
{
    public class TaskChangesDto
    {
        public Guid TaskId { get; set; }
        public string Content { get; set; }
    }
}