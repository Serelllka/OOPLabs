using System;

namespace Reports.DAL.DTO
{
    public class UpdateEmployeeDto
    {
        public Guid TaskId { get; set; }
        public Guid EmployeeId { get; set; }
    }
}