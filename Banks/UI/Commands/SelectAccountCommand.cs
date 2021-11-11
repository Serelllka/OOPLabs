using Banks.BusinessLogic.Accounts;
using Banks.UI.Menu;

namespace Banks.UI.Commands
{
    public class SelectAccountCommand : Command
    {
        private Account _account;
        public SelectAccountCommand(Menu.Menu currentMenu, Account account)
            : base(currentMenu)
        {
            _account = account;
        }

        public override Menu.Menu Execute()
        {
            return new AccountMenu(CurrentMenu, _account);
        }

        public override string ToString()
        {
            return _account.Id.ToString();
        }
    }
}