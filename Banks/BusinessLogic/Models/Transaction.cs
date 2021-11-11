using System;
using Banks.BusinessLogic.Accounts;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Models
{
    public class Transaction
    {
        private readonly Account _fromClient;
        private readonly Account _toClient;
        private readonly decimal _moneyAmount;
        private bool _status;

        public Transaction(Account from, Account to, decimal moneyAmount)
        {
            _fromClient = from;
            _toClient = to;
            _moneyAmount = moneyAmount;
            _status = true;
            Id = Guid.NewGuid();
        }

        private Transaction()
        { }

        public Guid Id { get; private set; }
        public bool Status => _status;

        public void CancelTransaction()
        {
            if (!_status)
            {
                throw new BanksException("This transaction is already canceled");
            }

            if (!CanCancel())
            {
                throw new BanksException("You can't cancel this transaction");
            }

            _fromClient.AddMoney(_moneyAmount);
            _toClient.GetMoney(_moneyAmount);
            _status = false;
        }

        private bool CanCancel()
        {
            return _toClient.CanWithdraw(_moneyAmount);
        }
    }
}