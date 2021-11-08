using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Accounts
{
    public class DepositAccount : Account
    {
        private PercentCalculator _percentCalculator;
        private bool _canWithdraw;

        public DepositAccount(Bank bank, Client client, PercentCalculator percentCalculator)
            : base(bank, client)
        {
            _canWithdraw = false;
            _percentCalculator = percentCalculator;
        }

        private DepositAccount()
        { }

        public void AccrueInterest()
        {
            MoneyAmount += MoneyAmount * _percentCalculator.GetPercent(MoneyAmount);
        }

        public void AllowWithdraw()
        {
            _canWithdraw = true;
        }

        public override void GetMoney(decimal money)
        {
            if (_canWithdraw is false)
            {
                throw new BanksException("You can't withdraw from this account");
            }

            if (money < 0)
            {
                throw new BanksException("You can't withdraw a negative amount");
            }

            if (money > MoneyAmount)
            {
                throw new BanksException("The amount you want to withdraw is more than it contains");
            }

            MoneyAmount -= money;
        }

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
            return "deposit";
        }
    }
}