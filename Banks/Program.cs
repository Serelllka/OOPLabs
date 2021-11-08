using System;
using System.Collections.Generic;
using System.Linq;
using Banks.BusinessLogic.Builders;
using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Services;
using Banks.BusinessLogic.ValueObject;
using Banks.Database.Contexts;
using Banks.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace Banks
{
    internal static class Program
    {
        private static void Main()
        {
            var context = new Context(
                new DbContextOptionsBuilder<Context>().
                    UseSqlite("Filename=banks.sqlite").
                    Options);

            context.UpdateDatabase();
            var cb = new CentralBank(context);
            var percentCalculator = new PercentCalculator();
            percentCalculator.AddInterestRate(new InterestRate(1000, 10));
            percentCalculator.AddInterestRate(new InterestRate(10000, 13));
            var clientBuilder = new ClientBuilder();
            clientBuilder.SetClientName("Igor");
            clientBuilder.SetClientSurname("Nikolaev");
            clientBuilder.SetPassport("12345");
            Client client = clientBuilder.Build();

            var bank1 = new Bank("Tinkoff", 20, 10, percentCalculator);
            bank1.CreateNewDebitAccount(client);
            var bank2 = new Bank("AlphaBank", 15, 12, percentCalculator);
            bank2.CreateNewDebitAccount(client);
            var bank3 = new Bank("Sberbank", 13, 9, percentCalculator);

            cb.RegisterNewBank(bank1);
            cb.RegisterNewBank(bank2);
            cb.RegisterNewBank(bank3);

            var menuManager = new MenuManager(cb);
            menuManager.Start();
        }
    }
}
