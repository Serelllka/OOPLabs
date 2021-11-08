using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Services;
using Banks.BuisnessLogic.ValueObject;
using Banks.Database.Contexts;
using Banks.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Banks
{
    internal static class Program
    {
        private static void Setup(Context context)
        {
            var cb = new CentralBank(context);
            var percentCalculator = new PercentCalculator();
            percentCalculator.AddInterestRate(new InterestRate(0, 7));
            percentCalculator.AddInterestRate(new InterestRate(10000, 10));
            var bank1 = new Bank("My bank", 10, 10, percentCalculator);
            var bank2 = new Bank("Lol", 11, 10, percentCalculator);
            var client1 = new Client("Igor", "Nikolaev", "1337");
            var client2 = new Client("Nikolay", "Baskov", "1488");
            bank1.CreateNewCreditAccount(client1);
            bank1.CreateNewCreditAccount(client2);
            bank2.CreateNewCreditAccount(client1);
            cb.RegisterNewBank(bank1);
            cb.RegisterNewBank(bank2);
        }

        private static void Main()
        {
            var context = new Context(
                new DbContextOptionsBuilder<Context>().
                    UseSqlite("Filename=banks.sqlite").
                    Options);

            // context.UpdateDatabase();
            var cb = new CentralBank(context);

            var menuManager = new MenuManager(cb);
            menuManager.Start();
        }
    }
}
