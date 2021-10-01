using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Time
    {
        private DateTime _date;
        public Time(int day, int hour, int minute)
        {
            if (day is > 6 or < 0)
            {
                throw new IsuExtraException("Incorrect value of the day");
            }

            if (hour is > 11 or < 6)
            {
                throw new IsuExtraException("Incorrect value of the hour");
            }

            if (minute is > 60 or < 0)
            {
                throw new IsuExtraException("Incorrect value of the minute");
            }

            _date = new DateTime(2228, 2, day, hour, minute, 0);
        }

        public int Day => _date.Day;
        public int Hour => _date.Hour;
        public int Minute => _date.Minute;
    }
}