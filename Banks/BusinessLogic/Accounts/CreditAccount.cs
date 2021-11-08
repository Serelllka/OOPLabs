using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Accounts
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