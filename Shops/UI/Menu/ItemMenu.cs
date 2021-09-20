using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ItemMenu : Menu
    {
        private Shop _shop;
        private Product _product;
        private ShopItem _shopItem;

        public ItemMenu(Shop shop, Product product, ShopItem shopItem, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu, shoppingList)
        {
            _shop = shop;
            _product = product;
            _shopItem = shopItem;
        }

        public override IMenu GenerateNextMenu()
        {
            if (Choice == "Add to card")
            {
                if (ShoppingList.All(item => !Equals(item.Item, _product)))
                {
                    ShoppingList.Add(new ShoppingListItem(_shop, _product, _shopItem.Price));
                }

                ShoppingList.FirstOrDefault(item => Equals(item.Item, _product)).Count += 1;
                return this;
            }

            if (Choice == "Remove from card")
            {
                ShoppingList.First(item => item.Item.Id == _product.Id).Count -= 1;
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
            ShoppingListItem obtainedItem = ShoppingList.FirstOrDefault(item => item.Item.Id == _product.Id);

            Table = new Table();
            SelectionOptions = new List<string>();
            if (obtainedItem == null || obtainedItem.Count < _shopItem.Count)
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
                    _product.Name,
                    _shopItem.Price.ToString(),
                    (_shopItem.Count - (obtainedItem?.Count ?? 0)).ToString());
        }
    }
}