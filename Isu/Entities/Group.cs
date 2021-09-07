using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        private List<Student> _students;
        public Group(string name)
        {
            if (!Regex.IsMatch(name, "M3[1-4][0-9][0-9]"))
            {
                throw new IsuException("Invalidate group name");
            }

            GroupName = name;
            _students = new List<Student>();
            Course = new CourseNumber(name[2] - '0');
            MaxStudentsAmount = 30;
        }

        public string GroupName { get; set; }
        public int MaxStudentsAmount { get; }
        public IReadOnlyList<Student> Students => _students;
        public CourseNumber Course { get; }

        public void AddStudent(Student newStudent)
        {
            if (Students.Count >= MaxStudentsAmount)
            {
                throw new IsuException("the maximum number of students has been exceeded");
            }

            _students.Add(newStudent);
        }

        public void RemoveStudent(Student student)
        {
            if (student == null)
            {
                throw new IsuException("the student doesnt exist");
            }

            _students.Remove(student);
        }
    }
}