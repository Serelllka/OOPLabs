using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class MainMenu : IMenu
    {
        private IMenu _prevMenu;
        private ShopManager _shopManager;
        private Person _person;
        private List<ShoppingListItem> _shoppingList;
        private List<string> _selectionOptions;

        public MainMenu(ShopManager shopManager, Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu = null)
        {
            _prevMenu = prevMenu;
            _person = person;
            _shoppingList = shoppingList;
            _shopManager = shopManager;
            UpdateTable();
        }

        public string Choice { get; private set; }

        public void Show()
        {
            UpdateTable();
            Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(_selectionOptions));
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == _selectionOptions[0])
            {
                return new CartMenu(_shopManager, _person, _shoppingList, this);
            }

            if (Choice == _selectionOptions[1])
            {
                return new ShopsMenu(_shopManager.Shops, _shoppingList, this);
            }

            if (_selectionOptions.Count > 2 && Choice == _selectionOptions[2])
            {
                return _prevMenu;
            }

            throw new System.NotImplementedException();
        }

        public void UpdateTable()
        {
            _selectionOptions = new List<string>() { "Shopping List", "List of shops" };
            if (_prevMenu != null)
            {
                _selectionOptions.Add("Back");
            }
        }
    }
}