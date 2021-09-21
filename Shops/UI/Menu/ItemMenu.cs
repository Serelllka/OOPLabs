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

        public override IMenu GenerateNextMenu()
        {
            if (Context.CurrentProduct is null)
            {
                throw new ShopException("CurrentProduct can't be null");
            }

            if (Choice == "Add to card")
            {
                if (ShoppingList.All(item => !Equals(item.Item, Context.CurrentProduct)))
                {
                    ShoppingList.Add(new ShoppingListItem(
                        Context.CurrentShop,
                        Context.CurrentProduct,
                        Context.CurrentShopItem.Price));
                }

                ShoppingList.First(item => Equals(item.Item, Context.CurrentProduct)).Count += 1;
                return this;
            }

            if (Choice == "Remove from card")
            {
                ShoppingList.First(item => item.Item.Id == Context.CurrentProduct.Id).Count -= 1;
                return this;
            }

            if (Choice == "Back")
            {
                return PrevMenu;
            }

            throw new ShopException("ItemMenu can't handle this choice");
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