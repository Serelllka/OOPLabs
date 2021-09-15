using System;
using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class ShopsMenu : IMenu
    {
        private Table _table;
        private IReadOnlyList<Shop> _shops;
        private List<ShoppingListItem> _shoppingList;
        private List<string> _selectionOptions;
        private IMenu _prevMenu;

        public ShopsMenu(IReadOnlyList<Shop> shops, List<ShoppingListItem> shoppingList, IMenu prevMenu)
        {
            _shops = shops;
            _prevMenu = prevMenu;
            _shoppingList = shoppingList;
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

            return new ProductsMenu(_shops[_selectionOptions.IndexOf(Choice)], _shoppingList, this);
        }

        public void UpdateTable()
        {
            _selectionOptions = new List<string>();
            _table = new Table();
            _table.AddColumns("N", "Name", "Different products");

            uint number = 1;
            foreach (Shop shop in _shops)
            {
                _table.AddRow(
                    number.ToString(),
                    shop.Name,
                    shop.GetListOfProducts().Count.ToString());
                _selectionOptions.Add(shop.Name);
                ++number;
            }

            _selectionOptions.Add("Back");
        }
    }
}