using System;
using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            if (courseNumber > 0 && courseNumber < 10)
            {
                Number = courseNumber;
            }
            else
            {
                throw new IsuException($"wrong range of course number: {Number}");
            }
        }

        public int Number { get; }
    }
}