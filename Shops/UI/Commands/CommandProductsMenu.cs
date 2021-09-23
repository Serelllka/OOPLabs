using Shops.UI.Menu;

namespace Shops.UI.Commands
{
    public class CommandProductsMenu : Command
    {
        public CommandProductsMenu(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            CurrentMenu.Context.CurrentShop =
                CurrentMenu.Context.Shops[CurrentMenu.SelectionOptions.IndexOf(CurrentMenu.Choice)];
            CurrentMenu = new ProductsMenu(
                CurrentMenu.Context.CurrentShop,
                CurrentMenu.ShoppingList,
                CurrentMenu.Context,
                CurrentMenu);
            return CurrentMenu;
        }
    }
}