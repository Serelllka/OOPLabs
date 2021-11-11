using Banks.BusinessLogic.Accounts;
using Spectre.Console;

namespace Banks.UI.Commands
{
    public class GetMoneyFromAccountCommand : Command
    {
        private Account _currentAccount;
        public GetMoneyFromAccountCommand(Menu.Menu currentMenu, Account account)
            : base(currentMenu)
        {
            _currentAccount = account;
        }

        public override Menu.Menu Execute()
        {
            decimal moneyAmount = AnsiConsole.Ask<decimal>("Enter money amount");
            _currentAccount.GetMoney(moneyAmount);
            return CurrentMenu;
        }

        public override string ToString()
        {
            return "Get money from account";
        }
    }
}