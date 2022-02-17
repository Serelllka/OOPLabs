using System;

namespace Reports.DAL.DTO
{
    public class ReportDto
    {
        public Guid TaskId { get; set; }
        public DateTime ResolveDate { get; set; }
    }
}