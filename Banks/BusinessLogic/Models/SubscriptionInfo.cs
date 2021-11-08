using System;

namespace Banks.BusinessLogic.Models
{
    public class SubscriptionInfo
    {
        public SubscriptionInfo(string info)
        {
            Info = info;
        }

        private SubscriptionInfo()
        { }

        public Guid Id { get; private set; }
        public string Info { get; private set; }
    }
}