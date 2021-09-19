using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public class MainMenu : Menu, IMenu
    {
        private ShopManager _shopManager;
        private Person _person;
        private List<ShoppingListItem> _shoppingList;

        public MainMenu(ShopManager shopManager, Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu = null)
            : base(prevMenu)
        {
            _person = person;
            _shoppingList = shoppingList;
            _shopManager = shopManager;
        }

        public IMenu GenerateNextMenu()
        {
            if (Choice == "List of shops")
            {
                return new ShopsMenu(_shopManager.Shops, _shoppingList, this);
            }

            if (Choice == "Shopping List")
            {
                return new CartMenu(_person, _shoppingList, this);
            }

            if (SelectionOptions.Count > 2 && Choice == SelectionOptions[2])
            {
                return PrevMenu;
            }

            throw new ShopException();
        }

        public override void UpdateTable()
        {
            SelectionOptions = new List<string> { "List of shops", "Shopping List" };
            if (PrevMenu != null)
            {
                SelectionOptions.Add("Back");
            }
        }
    }
}