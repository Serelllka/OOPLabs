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
            return money >= 0;
        }

        public override void AccrueInterest()
        { }
    }
}