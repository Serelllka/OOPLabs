using System.Collections.Generic;
using System.Data;
using Shops.Entities;
using Shops.Services;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class CartMenu : IMenu
    {
        private List<string> _selectionOptions;
        private Table _table;
        private IMenu _prevMenu;
        private List<ShoppingListItem> _shoppingList;
        public CartMenu(ShopManager shopManager, Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu)
        {
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
                return _prevMenu;
            throw new System.NotImplementedException();
        }

        public void UpdateTable()
        {
            _table = new Table();
            _selectionOptions = new List<string>();
            _table.AddColumns("N", "Title", "Amount", "Price");
            uint number = 1;
            foreach (ShoppingListItem item in _shoppingList)
            {
                _table.AddRow(number.ToString(), item.Name, item.Count.ToString(), item.Price.ToString());
                number += 1;
            }

            _selectionOptions.Add("Buy");
            _selectionOptions.Add("Back");
        }
    }
}