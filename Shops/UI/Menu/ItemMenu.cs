using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ItemMenu : Menu
    {
        public ItemMenu(
            List<ShoppingListItem> shoppingList,
            ClientContext context,
            Menu prevMenu)
            : base(prevMenu, shoppingList, context)
        {
        }

        public override void UpdateTable()
        {
            if (Context.CurrentProduct is null)
            {
                throw new ShopException("CurrentProduct can't be null");
            }

            ShoppingListItem obtainedItem = ShoppingList.FirstOrDefault(item => item.Item.Id == Context.CurrentProduct.Id);

            Table = new Table();
            SelectionOptions = new List<string>();
            if (obtainedItem == null || obtainedItem.Count < Context.CurrentShopItem.Count)
            {
                SelectionOptions.Add("Add to card");
            }

            if (obtainedItem != null && obtainedItem.Count > 0)
            {
                SelectionOptions.Add("Remove from card");
            }

            SelectionOptions.Add("Back");

            Table.AddColumns("N", "Name", "Price", "Count");

            Table.AddRow(
                    "1",
                    Context.CurrentProduct.Name,
                    Context.CurrentShopItem.Price.ToString(),
                    (Context.CurrentShopItem.Count - (obtainedItem?.Count ?? 0)).ToString());
        }
    }
}