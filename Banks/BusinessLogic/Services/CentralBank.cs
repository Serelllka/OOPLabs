using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Accounts;
using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Models;
using Banks.BusinessLogic.Tools;
using Banks.Database.Contexts;

namespace Banks.BusinessLogic.Services
{
    public class CentralBank
    {
        private Context _contex;
        private List<Bank> _banks;
        private List<Transaction> _transactions;

        public CentralBank(Context context)
        {
            _contex = context;
            context.LoadData();
            _banks = new List<Bank>();
            _banks = context.Banks.ToList();
            _transactions = new List<Transaction>();
        }

        public IReadOnlyCollection<Bank> Banks => _banks;

        public IReadOnlyCollection<Client> Clients
        {
            get
            {
                return _banks.SelectMany(bank => bank.Clients).Select(client => client).ToList();
            }
        }

        public void RegisterNewBank(Bank newBank)
        {
            if (newBank is null)
            {
                throw new BanksException("Bank can't be null");
            }

            if (_banks.Contains(newBank))
            {
                throw new BanksException("This bank is already implemented");
            }

            _banks.Add(newBank);
            _contex.Banks.Add(newBank);
            _contex.SaveChanges();
        }

        public Transaction MakeTransaction(Account accountFrom, Account accountTo, decimal money)
        {
            if (accountFrom == null || accountTo == null)
            {
                throw new BanksException("Account can't be null");
            }

            if (!accountFrom.CanWithdraw(money))
            {
                throw new BanksException("You can't withdraw from this account");
            }

            accountFrom.GetMoney(money);
            accountTo.AddMoney(money);
            var transaction = new Transaction(accountFrom, accountTo, money);
            _transactions.Add(transaction);
            _contex.Transactions.Add(transaction);
            _contex.SaveChanges();
            return transaction;
        }

        public void CancelTransaction(Transaction transaction)
        {
            if (!_transactions.Contains(transaction))
            {
                throw new BanksException("This transaction is not registered");
            }

            transaction.CancelTransaction();
        }

        public void DeleteBank(Bank bank)
        {
            if (bank is null)
            {
                throw new BanksException("Bank can't be null");
            }

            _banks.Remove(bank);
        }
    }
}