using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Isu.Tools;

namespace Isu.Entities
{
    public class Group
    {
        public Group(string name)
        {
            if (!Regex.IsMatch(name, "M3[1-4][0-9][0-9]"))
            {
                throw new IsuException("Invalidate group name");
            }

            GroupName = name;
            Students = new List<Student>();
            Course = new CourseNumber(name[2] - '0');
            System.Console.WriteLine(Course);
            MaxStudentsAmount = 30;
        }

        public string GroupName { get; set; }
        public int MaxStudentsAmount { get; }
        public List<Student> Students { get; }
        public CourseNumber Course { get; }

        public void AddStudent(Student newStudent)
        {
            if (Students.Count >= 30)
            {
                throw new IsuException("the maximum number of students has been exceeded");
            }

            Students.Add(newStudent);
        }
    }
}