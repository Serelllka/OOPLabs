using System.Collections.Generic;
using System.Runtime.InteropServices;
using Shops.Entities;
using Shops.Services;
using Shops.UI.Menu;
using Spectre.Console;

namespace Shops.UI
{
    public class MenuManager
    {
        private ShopManager _shopManager;
        private Person _person;

        public MenuManager(ShopManager shopManager, Person person)
        {
            _person = person;
            _shopManager = shopManager;
        }

        public void Start()
        {
            NextMenu(new MainMenu(_shopManager, _person, new List<ShoppingListItem>()));
        }

        public void NextMenu(IMenu currentMenu)
        {
            currentMenu.Show();
            currentMenu = currentMenu.GenerateNextMenu();
            AnsiConsole.Clear();
            NextMenu(currentMenu);
        }
    }
}