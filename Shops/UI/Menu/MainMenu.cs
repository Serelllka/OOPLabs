using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;

namespace Shops.UI.Menu
{
    public class MainMenu : Menu
    {
        private ShopManager _shopManager;
        private Person _person;

        public MainMenu(ShopManager shopManager, Person person, List<ShoppingListItem> shoppingList, IMenu prevMenu = null)
            : base(prevMenu, shoppingList)
        {
            _person = person;
            _shopManager = shopManager;
        }

        public override IMenu GenerateNextMenu()
        {
            if (Choice == "List of shops")
            {
                return new ShopsMenu(_shopManager.Shops, ShoppingList, this);
            }

            if (Choice == "Shopping List")
            {
                return new CartMenu(_person, ShoppingList, this);
            }

            if (Choice == "Back")
            {
                return PrevMenu;
            }

            if (Choice == "-")
            {
                return this;
            }

            throw new ShopException("MainMenu can't handle this choice");
        }

        public override void UpdateTable()
        {
            SelectionOptions = new List<string>();

            if (_shopManager.Shops.Count > 0)
            {
                SelectionOptions.Add("List of shops");
            }

            if (ShoppingList.Count > 0)
            {
                SelectionOptions.Add("Shopping List");
            }

            if (PrevMenu != null)
            {
                SelectionOptions.Add("Back");
            }

            if (SelectionOptions.Count == 1)
            {
                SelectionOptions.Add("-");  // this field is added because one field is not displayed correctly
            }
        }
    }
}