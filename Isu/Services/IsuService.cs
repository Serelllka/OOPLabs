﻿using System;
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
            Student foundedStudent = _students.SingleOrDefault(student => student.Id == id);

            if (foundedStudent == null)
                throw new IsuException("The student was not found");
            return foundedStudent;
        }

        public Student FindStudent(string name)
        {
            return _students.FirstOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return new List<Student>(_students.Where(student => student.StudentGroup.GroupName == groupName));
        }

        public List<Student> FindStudents(CourseNumber courseNumber) => new List<Student>(_students.Where(student => student.StudentGroup.Course.Number == courseNumber.Number));

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(group => group.GroupName == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(group => group.Course.Number == courseNumber.Number).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.StudentGroup.RemoveStudent(student);
            student.StudentGroup = newGroup;
            newGroup.AddStudent(student);
        }
    }
}