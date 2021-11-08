using System;

namespace Banks.BuisnessLogic.Models
{
    public class SubsriptionInfo
    {
        public SubsriptionInfo(string info)
        {
            Info = info;
        }

        private SubsriptionInfo()
        { }

        public Guid Id { get; private set; }
        public string Info { get; private set; }
    }
}