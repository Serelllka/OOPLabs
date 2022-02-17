using System;
using System.Threading.Tasks;

namespace Reports.DAL.Entities
{
    public class Report
    {
        private DateTime _resolveDate;
        private TaskModel _resolvedTask;

        private Report()
        {
        }
        public Report(Guid id, DateTime resolveDate, TaskModel resolvedTask)
        {
            Id = id;
            _resolveDate = resolveDate;
            _resolvedTask = resolvedTask;
        }

        public Guid Id { get; set; }
    }
}