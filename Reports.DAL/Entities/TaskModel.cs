using System;
using System.Collections.Generic;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class TaskModel
    {
        private List<Entry> _changes;
        
        public TaskModel(
            DateTime deadline,
            string name,
            string description)
        {
            State = TaskState.Ready;
            Id = Guid.NewGuid();
            TaskName = name;
            Deadline = deadline;
            Description = description;
            AssignedEmployee = null;
            _changes = new List<Entry>();
        }

        private TaskModel()
        {
        }

        public Guid Id { get; private init; }
        public string TaskName { get; private set; }
        public string Description { get; private set; } 
        public DateTime Deadline { get; private set; }
        public Employee AssignedEmployee { get; private set; }
        public TaskState State { get; set; }

        public void UpdateAssignedEmployee(Employee newEmployee)
        {
            AssignedEmployee = newEmployee;
            State = TaskState.InProcess;
        }

        public void CloseTask()
        {
            State = TaskState.Closed;
            AssignedEmployee = null;
        }
        
        public void AddChanges(Entry newEntry)
        {
            if (State == TaskState.InProcess)
            {
                _changes.Add(newEntry);   
            }
        }
    }
}