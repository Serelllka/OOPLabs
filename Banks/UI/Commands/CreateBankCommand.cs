using Banks.BusinessLogic.Entities;
using Banks.BusinessLogic.Services;
using Banks.BusinessLogic.ValueObject;
using Spectre.Console;

namespace Banks.UI.Commands
{
    public class CreateBankCommand : Command
    {
        private CentralBank _centralBank;
        public CreateBankCommand(Menu.Menu currentMenu, CentralBank centralBank)
            : base(currentMenu)
        {
            _centralBank = centralBank;
        }

        public override Menu.Menu Execute()
        {
            string bankName = AnsiConsole.Ask<string>("Enter bank name: ");
            decimal creditTax = AnsiConsole.Ask<decimal>("Enter credit tax: ");
            decimal debitPercent = AnsiConsole.Ask<decimal>("Enter debit percent: ");
            int counter = AnsiConsole.Ask<int>("Enter the number of credit rates on the deposit");
            var percentCalculator = new PercentCalculator();
            for (int i = 0; i < counter; ++i)
            {
                decimal accountBalance = AnsiConsole.Ask<decimal>(
                    "Enter the threshold value of the account balance: ");
                decimal percent = AnsiConsole.Ask<decimal>(
                    "Enter the percent of this value: ");
                percentCalculator.AddInterestRate(new InterestRate(accountBalance, percent));
            }

            int daysBeforeWithdraw = AnsiConsole.Ask<int>("Enter days before withdraw");

            _centralBank.RegisterNewBank(new Bank(
                bankName,
                creditTax,
                debitPercent,
                daysBeforeWithdraw,
                percentCalculator));

            return CurrentMenu;
        }

        public override string ToString()
        {
            return "Create new bank";
        }
    }
}