using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Accounts
{
    public class CreditAccount : Account
    {
        private readonly decimal _commission;
        public CreditAccount(Bank bank, Client client, decimal commission)
            : base(bank, client)
        {
            _commission = commission;
        }

        private CreditAccount()
        { }

        public override bool CanWithdraw(decimal money)
        {
            if (money < 0)
            {
                throw new BanksException("You can't withdraw");
            }

            return true;
        }

        public override string GetTypeInString()
        {
            return "credit";
        }
    }
}