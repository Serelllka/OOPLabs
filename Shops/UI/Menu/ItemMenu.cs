using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ItemMenu : IMenu
    {
        private IMenu _prevMenu;
        private Shop _shop;
        private List<ShoppingListItem> _shoppingList;
        private Product _product;
        private ShopItem _shopItem;
        private List<string> _selectionOptions;
        private Table _table;

        public ItemMenu(Shop shop, Product product, ShopItem shopItem, List<ShoppingListItem> shoppingList, IMenu prevMenu)
        {
            _shop = shop;
            _product = product;
            _shopItem = shopItem;
            _shoppingList = shoppingList;
            _prevMenu = prevMenu;
            UpdateTable();
        }

        public string Choice { get; private set; }

        public void Show()
        {
            UpdateTable();
            AnsiConsole.Render(_table);
            Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(_selectionOptions));
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == _selectionOptions[^1])
            {
                return _prevMenu;
            }

            if (Choice == "Add to card")
            {
                if (_shoppingList.All(item => item.Item != _product))
                {
                    _shoppingList.Add(new ShoppingListItem(_shop, _product, _shopItem.Price));
                }

                _shoppingList.FirstOrDefault(item => item.Item == _product).Count += 1;
                return this;
            }

            if (Choice == "Remove from card")
            {
                _shoppingList.FirstOrDefault(item => item.Item == _product).Count -= 1;
                return this;
            }

            throw new ShopException("ItemMenu can't handle this choice");
        }

        public void UpdateTable()
        {
            ShoppingListItem obtainedItem = _shoppingList.FirstOrDefault(item => item.Item == _product);

            _table = new Table();
            _selectionOptions = new List<string>();
            if (obtainedItem == null || obtainedItem.Count < _shopItem.Count)
            {
                _selectionOptions.Add("Add to card");
            }

            if (obtainedItem != null && obtainedItem.Count > 0)
            {
                _selectionOptions.Add("Remove from card");
            }

            _selectionOptions.Add("Back");

            _table.AddColumns("N", "Name", "Price", "Count");
            _table.AddRow(
                    "1",
                    _product.Name,
                    _shopItem.Price.ToString(),
                    (_shopItem.Count - (obtainedItem?.Count ?? 0)).ToString());
        }
    }
}