using System;
using System.Collections.Generic;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        private List<Employee> _subordinates;
        
        private Employee()
        {
        }

        public Employee(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "Id is invalid");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name is invalid");
            }

            Id = id;
            Name = name;
            _subordinates = new List<Employee>();
        }

        public void AddSubordinate(Employee subordinate)
        {
            if (this == subordinate)
            {
                throw new TaskException("Employee cannot be subordinate to himself");
            }
            _subordinates.Add(subordinate);
        }
        
        public Guid Id { get; private init; }
        public string Name { get; private init; }
    }
}