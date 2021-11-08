using Banks.BuisnessLogic.Entities;
using Banks.UI.Menu;

namespace Banks.UI.Commands
{
    public class SelectBankCommand : Command
    {
        private Bank _bank;
        public SelectBankCommand(Menu.Menu currentMenu, Bank bank)
            : base(currentMenu)
        {
            _bank = bank;
        }

        public override string ToString()
        {
            return _bank.Name;
        }

        public override Menu.Menu Execute()
        {
            return new BankMenu(CurrentMenu, _bank);
        }
    }
}