using System;

namespace Banks.BuisnessLogic.ValueObject
{
    public class InterestRate
    {
        public InterestRate(decimal accountBalance, decimal percent)
        {
            AccountBalance = accountBalance;
            Percent = percent;
        }

        private InterestRate()
        {
        }

        public Guid Id { get; private set; }
        public decimal AccountBalance { get; }
        public decimal Percent { get; }
    }
}