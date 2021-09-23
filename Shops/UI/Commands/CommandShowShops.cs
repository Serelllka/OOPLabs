using Shops.UI.Menu;

namespace Shops.UI.Commands
{
    public class CommandShowShops : Command
    {
        public CommandShowShops(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            CurrentMenu = new ShopsMenu(
                CurrentMenu.Context.Shops,
                CurrentMenu.ShoppingList,
                CurrentMenu.Context,
                CurrentMenu);
            return CurrentMenu;
        }
    }
}