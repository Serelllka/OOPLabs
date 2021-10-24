using System.Linq;
using Shops.Tools;

namespace Shops.UI.Commands
{
    public class CommandRemove : Command
    {
        public CommandRemove(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            if (CurrentMenu.Context.CurrentProduct is null)
            {
                throw new ShopException("CurrentProduct can't be null");
            }

            CurrentMenu.ShoppingList.First(item =>
                item.Item.Id == CurrentMenu.Context.CurrentProduct.Id).Count -= 1;
            return CurrentMenu;
        }
    }
}