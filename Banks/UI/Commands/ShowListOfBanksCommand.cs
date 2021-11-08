using Banks.BuisnessLogic.Services;
using Banks.UI.Menu;

namespace Banks.UI.Commands
{
    public class ShowListOfBanksCommand : Command
    {
        private CentralBank _centralBank;

        public ShowListOfBanksCommand(Menu.Menu currentMenu, CentralBank centralBank)
            : base(currentMenu)
        {
            _centralBank = centralBank;
        }

        public override Menu.Menu Execute()
        {
            return new ListOfBanksMenu(CurrentMenu, _centralBank);
        }

        public override string ToString()
        {
            return "Show List of banks";
        }
    }
}