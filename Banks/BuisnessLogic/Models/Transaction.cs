using Banks.BuisnessLogic.Accounts;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Models
{
    public class Transaction
    {
        private Account _fromClient;
        private Account _toClient;
        private decimal _moneyAmount;
        private bool _status;

        public Transaction(Account from, Account to, decimal moneyAmount)
        {
            _fromClient = from;
            _toClient = to;
            _moneyAmount = moneyAmount;
            _status = true;
        }

        private Transaction()
        { }

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
            if (_toClient.CanWithdraw(_moneyAmount))
            {
                return true;
            }

            return false;
        }
    }
}