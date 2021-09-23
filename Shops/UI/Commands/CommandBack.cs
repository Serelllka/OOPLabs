using Shops.UI.Models;

namespace Shops.UI.Commands
{
    public class CommandBack : Command
    {
        public CommandBack(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            return CurrentMenu.PrevMenu;
        }
    }
}