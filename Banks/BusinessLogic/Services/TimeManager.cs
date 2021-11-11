using System;

namespace Banks.BusinessLogic.Services
{
    public class TimeManager
    {
        public TimeManager()
        {
            CurrentDateTime = DateTime.Now;
        }

        public DateTime CurrentDateTime { get; private set; }

        public void SkipDay()
        {
            CurrentDateTime = CurrentDateTime.AddDays(1);
        }

        public void SkipMonth()
        {
            CurrentDateTime = CurrentDateTime.AddMonths(1);
        }
    }
}