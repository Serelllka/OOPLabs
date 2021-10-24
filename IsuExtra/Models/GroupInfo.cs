using Isu.Entities;
using IsuExtra.Entities;

namespace IsuExtra.Models
{
    public class GroupInfo
    {
        public GroupInfo(Schedule schedule, Group @group)
        {
            Timetable = schedule;
            StudentsGroup = group;
        }

        public Schedule Timetable { get; }
        public Group StudentsGroup { get; }
    }
}