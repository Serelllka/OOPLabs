using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups;
        private List<Student> _students;

        public IsuService()
        {
            _groups = new List<Group>();
            _students = new List<Student>();
        }

        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (!_groups.Contains(group))
            {
                throw new IsuException("This group doesn't exist");
            }

            var newStudent = new Student(group, name);
            _students.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(Guid id)
        {
            foreach (Student student in _students)
            {
                if (student.Uuid == id)
                {
                    return student;
                }
            }

            throw new NotImplementedException();
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _students)
            {
                if (student.Name == name)
                {
                    return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            return new List<Student>(_students.Where(student => student.StudentGroup.GroupName == groupName));
        }

        public List<Student> FindStudents(CourseNumber courseNumber) => new List<Student>(_students.Where(student => student.StudentGroup.Course.Number == courseNumber.Number));

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.GroupName == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => group.Course.Number == courseNumber.Number).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.StudentGroup.Students.Remove(student);
            student.StudentGroup = newGroup;
            newGroup.Students.Add(student);
        }
    }
}