using System.Collections.Generic;
using Banks.BuisnessLogic.Accounts;
using Banks.UI.Menu;

namespace Banks.UI.Commands
{
    public class ShowListOfAccountsCommand : Command
    {
        private IReadOnlyList<Account> _accounts;

        public ShowListOfAccountsCommand(Menu.Menu currentMenu, IReadOnlyList<Account> accounts)
            : base(currentMenu)
        {
            _accounts = accounts;
        }

        public override Menu.Menu Execute()
        {
            return new ListOfAccountsMenu(CurrentMenu, _accounts);
        }

        public override string ToString()
        {
            return "Show list of accounts of this client";
        }
    }
}