using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BuisnessLogic.Tools;
using Banks.BuisnessLogic.ValueObject;

namespace Banks.BuisnessLogic.Entities
{
    public class PercentCalculator
    {
        private List<InterestRate> _interestRate;

        public PercentCalculator()
        {
            _interestRate = new List<InterestRate>();
        }

        public Guid Id { get; private set; }

        public int GetNumberOfRates()
        {
            return _interestRate.Count();
        }

        public void AddInterestRate(InterestRate interestRate)
        {
            _interestRate.Add(interestRate);
            _interestRate = _interestRate.OrderBy(i => i.AccountBalance).Reverse().ToList();
        }

        public decimal GetPercent(decimal accountBalance)
        {
            InterestRate item = _interestRate.FirstOrDefault(
                item => accountBalance >= item.AccountBalance);

            if (item is null)
            {
                throw new BanksException("can't calculate percent");
            }

            return item.Percent;
        }
    }
}