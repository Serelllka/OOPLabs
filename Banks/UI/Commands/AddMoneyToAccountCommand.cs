using Banks.BusinessLogic.Accounts;
using Spectre.Console;

namespace Banks.UI.Commands
{
    public class AddMoneyToAccountCommand : Command
    {
        private Account _currentAccount;
        public AddMoneyToAccountCommand(Menu.Menu currentMenu, Account account)
            : base(currentMenu)
        {
            _currentAccount = account;
        }

        public override Menu.Menu Execute()
        {
            decimal moneyAmount = AnsiConsole.Ask<decimal>("Enter additional money");
            _currentAccount.AddMoney(moneyAmount);
            return CurrentMenu;
        }

        public override string ToString()
        {
            return "Add money to account";
        }
    }
}