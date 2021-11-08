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
        private static void Main()
        {
            var context = new Context(
                new DbContextOptionsBuilder<Context>().
                    UseSqlite("Filename=banks.sqlite").
                    Options);

            context.UpdateDatabase();
            var cb = new CentralBank(context);

            // var menuManager = new MenuManager(cb);
            // menuManager.Start();
        }
    }
}
