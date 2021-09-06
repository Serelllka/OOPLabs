using System;

namespace Isu.Entities
{
    public class Student
    {
        public Student(Group studentGroup, string name)
        {
            Name = name;
            Uuid = Guid.NewGuid();
            StudentGroup = studentGroup;
        }

        public Guid Uuid { get; }
        public string Name { get; }
        public Group StudentGroup { get; set; }
    }
}