using System;
using System.Collections.Generic;
using Banks.BuisnessLogic.Models;

namespace Banks.BuisnessLogic.Entities
{
    public class Client : IEquatable<Client>
    {
        private List<SubscriptionInfo> _subscriptionInfo;

        public Client()
        { }
        public Client(string name, string surname, string passportId)
        {
            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            PassportId = passportId;
            _subscriptionInfo = new List<SubscriptionInfo>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string PassportId { get; private set; }
        public IReadOnlyList<SubscriptionInfo> UserLog => _subscriptionInfo;

        public bool Equals(Client other)
        {
            return other is not null && other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Client);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public void AddSubscriptionInfo(SubscriptionInfo subscriptionInfo)
        {
            _subscriptionInfo.Add(subscriptionInfo);
        }
    }
}