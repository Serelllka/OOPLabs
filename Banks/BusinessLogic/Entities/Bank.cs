using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Accounts;
using Banks.BusinessLogic.Models;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Entities
{
    public class Bank
    {
        private List<Account> _accounts;
        private List<Client> _clients;
        private decimal _creditTax;
        private decimal _debitPercent;
        private int _daysBeforeWithdraw;
        private PercentCalculator _percentCalculator;

        public Bank()
        { }
        public Bank(
            string name,
            decimal creditTax,
            decimal debitPercent,
            int daysBeforeWithdraw,
            PercentCalculator percentCalculator)
        {
            Name = name ?? throw new BanksException("name can't be null");
            _daysBeforeWithdraw = daysBeforeWithdraw;
            if (daysBeforeWithdraw < 0)
            {
                throw new BanksException("daysBeforeWithdraw must be positive");
            }

            _accounts = new List<Account>();
            _clients = new List<Client>();
            _creditTax = creditTax;
            _debitPercent = debitPercent;
            _percentCalculator = percentCalculator
                                 ?? throw new BanksException("percentCalculator can't be null");
            Id = Guid.NewGuid();
        }

        public string Name { get; private set; }
        public Guid Id { get; private set; }
        public IReadOnlyCollection<Client> Clients => _clients;
        public IReadOnlyCollection<Account> Accounts => _accounts;

        public CreditAccount CreateNewCreditAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            var creditAccount = new CreditAccount(this, client, _creditTax);
            _accounts.Add(creditAccount);
            return creditAccount;
        }

        public DebitAccount CreateNewDebitAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            var debitAccount = new DebitAccount(this, client, _debitPercent);
            _accounts.Add(debitAccount);
            return debitAccount;
        }

        public DepositAccount CreateNewDepositAccount(Client client)
        {
            if (client is null)
            {
                throw new BanksException("client can't be null");
            }

            if (!_clients.Contains(client))
            {
                _clients.Add(client);
            }

            var depositAccount = new DepositAccount(
                this,
                client,
                _percentCalculator,
                DateTime.Now.AddDays(_daysBeforeWithdraw));
            _accounts.Add(depositAccount);
            return depositAccount;
        }

        public IReadOnlyList<Account> GetListOfClientsAccounts(Client client)
        {
            return _accounts.Where(item => item.OwnerClient.Equals(client)).ToList();
        }

        public void ChangeCreditTax(decimal newCreditTax)
        {
            if (newCreditTax < 0)
            {
                throw new BanksException("Credit tax can't be negative");
            }

            _creditTax = newCreditTax;

            var list = _clients.Where(HasAccount<CreditAccount>).ToList();
            list.ForEach(item => item.AddSubscriptionInfo(
                new SubscriptionInfo("Credit tax was changed")));
        }

        private bool HasAccount<T>(Client client)
            where T : Account
        {
            return GetListOfClientsAccounts(client).
                Any(item => item is T);
        }
    }
}