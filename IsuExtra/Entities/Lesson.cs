using System;
using IsuExtra.Models;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(int startingDay, int startingHour, int startingMinute, int duration = 90)
        {
            StartingTime = new Time(startingDay, startingHour, startingMinute);
            EndingTime = new Time(
                startingDay,
                startingHour + ((startingMinute + duration) / 60),
                (startingMinute + duration) % 60);
        }

        public Time StartingTime { get; set; }
        public Time EndingTime { get; }

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