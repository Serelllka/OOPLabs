using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BuisnessLogic.Accounts;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Entities
{
    public class Bank
    {
        private List<Account> _accounts;
        private List<Client> _clients;
        private decimal _creditTax;
        private decimal _debitPercent;
        private PercentCalculator _percentCalculator;

        public Bank()
        { }
        public Bank(
            string name,
            decimal creditTax,
            decimal debitPercent,
            PercentCalculator percentCalculator)
        {
            Name = name;
            _accounts = new List<Account>();
            _clients = new List<Client>();
            _creditTax = creditTax;
            _debitPercent = debitPercent;
            _percentCalculator = percentCalculator;
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public IReadOnlyCollection<Client> Clients => _clients;

        public void CreateNewCreditAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            _accounts.Add(new CreditAccount(this, client, _creditTax));
        }

        public void CreateNewDebitAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            _accounts.Add(new DebitAccount(this, client, _creditTax));
        }

        public void CreateNewDepositAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            _accounts.Add(new DepositAccount(this, client, _percentCalculator));
        }

        public IReadOnlyList<Account> GetListOfClientsAccounts(Client client)
        {
            return _accounts.Where(item => item.OwnerClient.Equals(client)).ToList();
        }
    }
}