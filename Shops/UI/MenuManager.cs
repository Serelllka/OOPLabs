using System;
using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI.Menu;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI
{
    public class MenuManager
    {
        private ShopManager _shopManager;
        private Person _person;
        private MenuFactory _factory;

        public MenuManager(ShopManager shopManager, Person person)
        {
            _person = person ?? throw new ShopException("Person can't be null");
            _shopManager = shopManager ?? throw new ShopException("ShopManager can't be null");
        }

        public void Start()
        {
            _factory = new MenuFactory(new MainMenu(
                new List<ShoppingListItem>(),
                new ClientContext(_shopManager, _person)));
            NextMenu(_factory.CurrentMenu);
        }

        public void NextMenu(Menu.Menu currentMenu)
        {
            currentMenu.Show();

            // currentMenu = currentMenu.GenerateNextMenu();
            // NextMenu(currentMenu);
            AnsiConsole.Clear();
            NextMenu(_factory.CreateMenu(currentMenu.Choice));
        }
    }
}