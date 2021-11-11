using System;
using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Accounts
{
    public class DepositAccount : Account
    {
        private PercentCalculator _percentCalculator;
        private DateTime _withdrawDateTime;
        private bool _isWithdrawable;

        public DepositAccount(
            Bank bank,
            Client client,
            PercentCalculator percentCalculator,
            DateTime withdrawDateTime)
            : base(bank, client)
        {
            _withdrawDateTime = withdrawDateTime;
            _isWithdrawable = false;
            _percentCalculator = percentCalculator;
        }

        private DepositAccount()
        { }

        public override void AccrueInterest()
        {
            MoneyAmount += MoneyAmount * _percentCalculator.GetPercent(MoneyAmount);
        }

        public void UpdateWithdrawStatus(DateTime currDateTime)
        {
            if (currDateTime > _withdrawDateTime)
            {
                _isWithdrawable = true;
            }
        }

        public override void GetMoney(decimal money)
        {
            if (!_isWithdrawable)
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
    }
}