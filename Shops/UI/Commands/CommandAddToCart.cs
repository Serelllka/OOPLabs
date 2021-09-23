using System.Linq;
using Shops.Entities;

namespace Shops.UI.Commands
{
    public class CommandAddToCart : Command
    {
        public CommandAddToCart(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            if (CurrentMenu.ShoppingList.All(item =>
                !Equals(item.Item, CurrentMenu.Context.CurrentProduct)))
            {
                CurrentMenu.ShoppingList.Add(new ShoppingListItem(
                    CurrentMenu.Context.CurrentShop,
                    CurrentMenu.Context.CurrentProduct,
                    CurrentMenu.Context.CurrentShopItem.Price));
            }

            CurrentMenu.ShoppingList.First(item =>
                Equals(item.Item, CurrentMenu.Context.CurrentProduct)).Count += 1;
            return CurrentMenu;
        }
    }
}