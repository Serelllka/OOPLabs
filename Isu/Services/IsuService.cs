using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private List<Group> _groupsList;
        private List<Student> _studentsList;

        public IsuService()
        {
            _groupsList = new List<Group>();
            _studentsList = new List<Student>();
        }

        public Group AddGroup(string name)
        {
            var newGroup = new Group(name);
            _groupsList.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            var newStudent = new Student(group, name);
            _studentsList.Add(newStudent);
            return newStudent;
        }

        public Student GetStudent(Guid id)
        {
            foreach (Student student in _studentsList)
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
            foreach (Student student in _studentsList)
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
            return new List<Student>(_studentsList.Where(student => student.StudentGroup.GroupName == groupName));
        }

        public List<Student> FindStudents(CourseNumber courseNumber) => new List<Student>(_studentsList.Where(student => Convert.ToInt32(student.StudentGroup.GroupName[1]) == courseNumber.Number));

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groupsList)
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
            return new List<Group>(_groupsList.Where(group => Convert.ToInt32(group.GroupName[1]) == courseNumber.Number));
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.StudentGroup.ListOfStudents.Remove(student);
            student.StudentGroup = newGroup;
            newGroup.ListOfStudents.Add(student);
        }
    }
}