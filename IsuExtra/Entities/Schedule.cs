using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Schedule
    {
        private List<Lesson> _lessons;

        public Schedule()
        {
            Id = Guid.NewGuid();
            _lessons = new List<Lesson>();
        }

        public Guid Id { get; }

        public void AddLesson(Lesson lesson)
        {
            if (!CanAddThisLesson(lesson))
            {
                throw new IsuExtraException("Can't add this lesson");
            }

            _lessons.Add(lesson);
        }

        public bool Stacked(Schedule schedule)
        {
            return _lessons.Exists(item => !schedule.CanAddThisLesson(item));
        }

        public bool CanAddThisLesson(Lesson lesson)
        {
            if (lesson is null)
            {
                throw new IsuExtraException("lesson is not exist");
            }

            return !_lessons.Contains(lesson) && _lessons.All(item => !item.Stacked(lesson));
        }
    }
}