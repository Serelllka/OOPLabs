using Shops.UI.Models;

namespace Shops.UI.Commands
{
    public abstract class Command
    {
        public Command(Menu.Menu currentMenu)
        {
            CurrentMenu = currentMenu;
        }

        protected Menu.Menu CurrentMenu { get; set; }

        public abstract Menu.Menu Execute();
    }
}