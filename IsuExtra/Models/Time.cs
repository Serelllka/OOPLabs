using IsuExtra.Tools;

namespace IsuExtra.Models
{
    public class Time
    {
        public Time(uint day, uint hour, uint minute)
        {
            if (day > 6)
            {
                throw new IsuExtraException("Incorrect value of the day");
            }

            if (hour > 11 || hour < 6)
            {
                throw new IsuExtraException("Incorrect value of the hour");
            }

            if (minute > 60)
            {
                throw new IsuExtraException("Incorrect value of the minute");
            }

            Day = day;
            Hour = hour;
            Minute = minute;
        }

        public uint Day { get; }
        public uint Hour { get; }
        public uint Minute { get; }
    }
}