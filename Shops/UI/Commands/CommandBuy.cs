using Shops.Entities;

namespace Shops.UI.Commands
{
    public class CommandBuy : Command
    {
        public CommandBuy(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            foreach (ShoppingListItem item in CurrentMenu.ShoppingList)
            {
                item.BuyThisItem(CurrentMenu.Context.Customer);
            }

            return CurrentMenu;
        }
    }
}