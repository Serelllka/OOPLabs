using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Isu.Entities;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Entities;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class UniversityManager
    {
        private List<Faculty> _faculties;
        private IsuService _isuService;
        private Dictionary<Faculty, GsaCourse> _courses;

        public UniversityManager()
        {
            _isuService = new IsuService();
            _faculties = new List<Faculty>();
            _courses = new Dictionary<Faculty, GsaCourse>();
        }

        public void RegisterFaculty(Faculty faculty)
        {
            if (faculty is null)
            {
                throw new IsuExtraException("this faculty is not exist");
            }

            if (_faculties.Contains(faculty))
            {
                throw new IsuExtraException("this faculty is already added");
            }

            _faculties.Add(faculty);
        }

        public IReadOnlyList<Student> GetListOfStudentsWithoutGsa(Group @group)
        {
            return _isuService.FindStudents(group.GroupName).Where(item => !StudentHasGsa(item)).ToList();
        }

        public void RegisterGsaCourse(GsaCourse course, Faculty faculty)
        {
            if (faculty is null)
            {
                throw new IsuExtraException("faculty can't be null");
            }

            if (course is null)
            {
                throw new IsuExtraException("course can't be null");
            }

            if (!_faculties.Contains(faculty))
            {
                throw new IsuExtraException("This faculty not registered in system");
            }

            if (_courses.ContainsValue(course))
            {
                throw new IsuExtraException("This courses is already registered in system");
            }

            if (_courses.ContainsKey(faculty))
            {
                throw new IsuExtraException("This courses is already registered in system");
            }

            _courses.Add(faculty, course);
        }

        public Group CreateGroup(Faculty faculty, Schedule schedule, string groupName)
        {
            if (faculty is null)
            {
                throw new IsuExtraException("Faculty can't be null");
            }

            if (!_faculties.Contains(faculty))
            {
                throw new IsuExtraException("This faculty should be registered in this service");
            }

            if (_faculties.Exists(item => item.ContainsSchedule(schedule)))
            {
                throw new IsuExtraException("Schedule is already added");
            }

            Group newGroup = _isuService.AddGroup(groupName);
            faculty.AddGroup(schedule, newGroup);
            return newGroup;
        }

        public Student CreateStudent(Group @group, string studentName)
        {
            if (group is null)
            {
                throw new IsuExtraException("Group can't be null");
            }

            if (studentName is null)
            {
                throw new IsuExtraException("Student name can't be null");
            }

            if (!_faculties.Exists(item => item.ContainsGroup(group)))
            {
                throw new IsuExtraException("This group is not exist in this university");
            }

            return _isuService.AddStudent(group, studentName);
        }

        public void RegisterStudentOnGsa(GsaFlow gsaFlow, Student student)
        {
            if (gsaFlow is null)
            {
                throw new IsuExtraException("This GSA is not exist");
            }

            if (!ContainsGsa(gsaFlow.Course))
            {
                throw new IsuExtraException("This GSA not from this university");
            }

            if (Equals(FindFacultyByGsa(gsaFlow.Course), FindFacultyByGroup(student.StudentGroup)))
            {
                throw new IsuExtraException("Can't register student to GSA from his faculty");
            }

            if (gsaFlow.GsaSchedule.Stacked(GetGroupInfo(student.StudentGroup).Timetable))
            {
                throw new IsuExtraException("Can't register student to GSA intersect schedules");
            }

            gsaFlow.RegisterStudent(student);
        }

        public void CancelRegistrationOnGsa(GsaCourse gsaCourse, Student student)
        {
            if (gsaCourse is null)
            {
                throw new IsuExtraException("GSA can't be null");
            }

            if (student is null)
            {
                throw new IsuExtraException("student can't be null");
            }

            gsaCourse.GetFlowByStudent(student).RemoveStudent(student);
        }

        public bool StudentHasGsa(Student student)
        {
            return _faculties.Exists(item => _courses[item].ContainsStudent(student));
        }

        private bool ContainsGsa(GsaCourse gsaCourse)
        {
            return _courses.ContainsValue(gsaCourse);
        }

        private Faculty FindFacultyByGsa(GsaCourse course)
        {
            return _courses.FirstOrDefault(item => Equals(item.Value, course)).Key;
        }

        private Faculty FindFacultyByGroup(Group @group)
        {
            return _faculties.FirstOrDefault(item => item.ContainsGroup(group))
                   ?? throw new IsuExtraException("this group is not registered in the system");
        }

        private GroupInfo GetGroupInfo(Group @group)
        {
            return _faculties.
                Select(item => item.FindGroupInfo(group)).
                FirstOrDefault(item => item is not null)
                   ?? throw new IsuExtraException("can't get GroupInfo");
        }
    }
}