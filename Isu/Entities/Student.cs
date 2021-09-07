using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class Student
    {
        public Student(Group studentGroup, string name)
        {
            StudentGroup = studentGroup ?? throw new IsuException("Group is null");
            Name = name;
            Id = Guid.NewGuid();
            studentGroup.AddStudent(this);
        }

        public Guid Id { get; }
        public string Name { get; }
        public Group StudentGroup { get; set; }
    }
}