using System.Collections.Generic;
using System.Data;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class CartMenu : IMenu
    {
        private ShopManager _shopManager;
        private List<string> _selectionOptions;
        private Table _table;
        private Person _person;
        private IMenu _prevMenu;
        private List<ShoppingListItem> _shoppingList;
        public CartMenu(ShopManager shopManager, Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu)
        {
            _person = person;
            _shopManager = shopManager;
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

            if (Choice == "Buy")
            {
                foreach (ShoppingListItem item in _shoppingList)
                {
                    item.BuyThisItem(_person);
                }

                return this;
            }

            if (Choice == "-")
            {
                return this;
            }

            throw new ShopException("CartMenu can't handle this choice");
        }

        public void UpdateTable()
        {
            _table = new Table();
            _selectionOptions = new List<string>();
            _table.AddColumns("N", "Title", "Amount", "Price", "Total");
            uint number = 1;
            uint totalPrice = 0;
            foreach (ShoppingListItem item in _shoppingList)
            {
                _table.AddRow(
                    number.ToString(),
                    item.Name,
                    item.Count.ToString(),
                    item.Price.ToString(),
                    (item.Count * item.Price).ToString());
                totalPrice += item.Count * item.Price;
                number += 1;
            }

            _table.AddRow("-", "-", "-", "-", "-");
            _table.AddRow("Person:", _person.Name, "Balance:", _person.Balance.ToString());
            if (_person.Balance < totalPrice)
            {
                _table.AddRow("Not enough money ", "Total price:", totalPrice.ToString(), "-", "-");
                _selectionOptions.Add("-"); // this field is added because one field is not displayed correctly
            }
            else
            {
                _selectionOptions.Add("Buy");
            }

            _selectionOptions.Add("Back");
        }
    }
}