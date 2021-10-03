using System;
using IsuExtra.Models;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(DateTime startingTime, DateTime endingTime)
        {
            if (endingTime < startingTime)
            {
                throw new IsuExtraException("startingTime should be before endingTime");
            }

            if (Math.Abs((endingTime - startingTime).TotalMinutes - 90) < 0.1)
            {
                throw new IsuExtraException("Lesson should last 90 minutes");
            }

            StartingTime = new Time(startingTime);
            EndingTime = new Time(endingTime);
        }

        public Time StartingTime { get; set; }
        public Time EndingTime { get; set; }

        public bool Stacked(Lesson lesson)
        {
            if (StartingTime.Day != lesson.StartingTime.Day)
            {
                return false;
            }

            if (StartingTime.Hour > lesson.EndingTime.Hour
                || (StartingTime.Hour == lesson.EndingTime.Hour && StartingTime.Minute > lesson.EndingTime.Minute))
            {
                return false;
            }

            if (EndingTime.Hour < lesson.StartingTime.Hour
                || (EndingTime.Hour == lesson.StartingTime.Hour && EndingTime.Minute < lesson.StartingTime.Minute))
            {
                return false;
            }

            return true;
        }
    }
}