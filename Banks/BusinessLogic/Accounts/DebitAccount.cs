﻿using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Tools;

namespace Banks.BusinessLogic.Accounts
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
                return false;
            }

            return money <= MoneyAmount;
        }

        public override void AccrueInterest()
        {
            MoneyAmount += MoneyAmount * _percent;
        }
    }
}