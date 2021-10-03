using System;
using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Time
    {
        private DateTime _date;
        public Time(DateTime dateTime)
        {
            if (dateTime.Hour is > 22 or < 6)
            {
                throw new IsuExtraException("Incorrect value of the hour");
            }

            _date = dateTime;
        }

        public int Day => _date.Day;
        public int Hour => _date.Hour;
        public int Minute => _date.Minute;
    }
}