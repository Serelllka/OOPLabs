using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DAL.Tools;

namespace Reports.DAL.Entities
{
    public class Employee
    {
        private readonly List<Employee> _subordinates;
        
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
        
        public Guid Id { get; private init; }
        public string Name { get; private init; }

        public void AddSubordinate(Employee subordinate)
        {
            if (this == subordinate)
            {
                throw new ReportException("Employee cannot be subordinate to himself");
            }
            _subordinates.Add(subordinate);
        }

        public List<Employee> GetListOfAllSubordinates()
        {
            var allSubs = new List<Employee>();
            allSubs.AddRange(_subordinates);
            foreach (Employee sub in _subordinates)
            {
                allSubs.AddRange(sub.GetListOfAllSubordinates());
            }

            return allSubs;
        }

        public bool IsSubordinate(Employee employee)
        {
            return _subordinates.Contains(employee) ||
                   _subordinates.Any(item => item.IsSubordinate(employee));
        }
    }
}