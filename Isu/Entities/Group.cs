using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private List<Student> _listOfStudents;

        public Group(string name)
        {
            if (!Regex.IsMatch(name, "M3[1-4][0-9][0-9]"))
            {
                throw new IsuException("Invalidate group name");
            }

            GroupName = name;
            Guid.NewGuid();
            ListOfStudents = new List<Student>();
        }

        public string GroupName { get; set; }

        public List<Student> ListOfStudents
        {
            get => _listOfStudents;
            set => _listOfStudents = value;
        }

        public void AddStudent(Student newStudent)
        {
            ListOfStudents.Add(newStudent);
        }
    }
}