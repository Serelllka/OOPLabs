using Shops.UI.Menu;

namespace Shops.UI.Commands
{
    public class CommandItemMenu : Command
    {
        public CommandItemMenu(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            CurrentMenu.Context.CurrentProduct =
                CurrentMenu.Context.GetCurrentProducts()[CurrentMenu.SelectionOptions.IndexOf(CurrentMenu.Choice)];
            CurrentMenu = new ItemMenu(
                CurrentMenu.ShoppingList,
                CurrentMenu.Context,
                CurrentMenu);
            return CurrentMenu;
        }
    }
}