using Shops.UI.Menu;

namespace Shops.UI.Commands
{
    public class CommandShowShoppingList : Command
    {
        public CommandShowShoppingList(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            CurrentMenu = new CartMenu(
                CurrentMenu.ShoppingList,
                CurrentMenu.Context,
                CurrentMenu);
            return CurrentMenu;
        }
    }
}