using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ItemMenu : Menu, IMenu
    {
        private Table _table;

        private Shop _shop;
        private List<ShoppingListItem> _shoppingList;
        private Product _product;
        private ShopItem _shopItem;

        public ItemMenu(Shop shop, Product product, ShopItem shopItem, List<ShoppingListItem> shoppingList, IMenu prevMenu)
            : base(prevMenu)
        {
            _shop = shop;
            _product = product;
            _shopItem = shopItem;
            _shoppingList = shoppingList;
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == SelectionOptions[^1])
            {
                return PrevMenu;
            }

            if (Choice == "Add to card")
            {
                if (_shoppingList.All(item => !Equals(item.Item, _product)))
                {
                    _shoppingList.Add(new ShoppingListItem(_shop, _product, _shopItem.Price));
                }

                _shoppingList.FirstOrDefault(item => Equals(item.Item, _product)).Count += 1;
                return this;
            }

            if (Choice == "Remove from card")
            {
                _shoppingList.First(item => item.Item.Id == _product.Id).Count -= 1;
                return this;
            }

            throw new ShopException("ItemMenu can't handle this choice");
        }

        public override void UpdateTable()
        {
            ShoppingListItem obtainedItem = _shoppingList.FirstOrDefault(item => item.Item.Id == _product.Id);

            _table = new Table();
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

            _table.AddColumns("N", "Name", "Price", "Count");
            _table.AddRow(
                    "1",
                    _product.Name,
                    _shopItem.Price.ToString(),
                    (_shopItem.Count - (obtainedItem?.Count ?? 0)).ToString());
        }
    }
}