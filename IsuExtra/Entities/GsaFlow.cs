using System.Collections.Generic;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class GsaFlow
    {
        private List<Student> _students;

        public GsaFlow(Schedule schedule, GsaCourse course)
        {
            Course = course;
            GsaSchedule = schedule;
            _students = new List<Student>();
        }

        public IReadOnlyList<Student> Students { get; }
        public GsaCourse Course { get; }
        public Schedule GsaSchedule { get;  }

        public bool ContainsStudent(Student student)
        {
            return _students.Contains(student);
        }

        public void RegisterStudent(Student student)
        {
            if (student is null)
            {
                throw new IsuExtraException("student can't be null");
            }

            _students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (student is null)
            {
                throw new IsuExtraException("student can't be null");
            }

            if (!_students.Contains(student))
            {
                throw new IsuExtraException("this student is not contained in this GSA");
            }

            _students.Remove(student);
        }
    }
}