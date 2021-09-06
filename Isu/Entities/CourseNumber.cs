using Isu.Tools;

namespace Isu.Entities
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            if (Number > 0 && Number < 10)
            {
                Number = courseNumber;
            }
            else
            {
                throw new IsuException("wrong range of course number");
            }
        }

        public int Number { get; }
    }
}