using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Faculty
    {
        private List<GroupInfo> _groups;
        public Faculty(string name)
        {
            Name = name ?? throw new IsuExtraException("name can't be null");
            _groups = new List<GroupInfo>();
            Id = Guid.NewGuid();
            GsaCourse = null;
        }

        public string Name { get; }
        public GsaCourse GsaCourse { get; set; }
        public Guid Id { get; }
        public bool ContainsGroup(Group @group)
        {
            if (group is null)
            {
                throw new IsuExtraException("group can't be null");
            }

            return _groups.Exists(item => Equals(item.StudentsGroup, group));
        }

        public bool ContainsSchedule(Schedule schedule)
        {
            if (schedule is null)
            {
                throw new IsuExtraException("schedule can't be null");
            }

            return _groups.Exists(item => Equals(item.Timetable, schedule));
        }

        public void AddGroup(Schedule schedule, Group @group)
        {
            if (ContainsSchedule(schedule))
            {
                throw new IsuExtraException("this schedule is already added");
            }

            if (ContainsGroup(group))
            {
                throw new IsuExtraException("this group is already added");
            }

            _groups.Add(new GroupInfo(schedule, group));
        }

        public GroupInfo FindGroupInfo(Group @group)
        {
            return _groups.FirstOrDefault(item => Equals(item.StudentsGroup, group));
        }
    }
}