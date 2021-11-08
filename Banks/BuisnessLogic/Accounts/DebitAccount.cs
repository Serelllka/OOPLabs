using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Accounts
{
    public class DebitAccount : Account
    {
        private decimal _percent;

        public DebitAccount(Bank bank, Client client, decimal procent)
            : base(bank, client)
        {
            _percent = procent;
        }

        private DebitAccount()
        { }

        public override bool CanWithdraw(decimal money)
        {
            if (money < 0)
            {
                throw new BanksException("You can't withdraw");
            }

            return money <= MoneyAmount;
        }

        public override string GetTypeInString()
        {
            return "debit";
        }
    }
}