﻿using System;
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
        }

        public string Name { get; }
        public Guid Id { get; }
        public bool ContainsGroup(Group @group)
        {
            if (group is null)
            {
                throw new IsuExtraException("group can't be null");
            }

            return _groups.Exists(item => item.StudentsGroup == group);
        }

        public bool ContainsStackedSchedule(Schedule schedule)
        {
            if (schedule is null)
            {
                throw new IsuExtraException("schedule can't be null");
            }

            return _groups.Exists(item => item.Timetable.Stacked(schedule));
        }

        public void AddGroup(Group @group, Schedule schedule)
        {
            if (ContainsStackedSchedule(schedule))
            {
                throw new IsuExtraException("this schedule can't be added");
            }

            if (ContainsGroup(group))
            {
                throw new IsuExtraException("this group is already added");
            }

            _groups.Add(new GroupInfo(schedule, group));
        }

        public GroupInfo FindGroupInfo(Group @group)
        {
            return _groups.FirstOrDefault(item => item.StudentsGroup == group);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is Faculty faculty && Id == faculty.Id;
        }
    }
}