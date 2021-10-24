using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            if (courseNumber < 1 || courseNumber > 9)
            {
                throw new IsuException($"wrong range of course number: {Number}");
            }

            Number = courseNumber;
        }

        public int Number { get; }
    }
}