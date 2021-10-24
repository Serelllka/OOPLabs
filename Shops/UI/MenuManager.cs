using System;
using System.Collections.Generic;
using Shops.Entities;
using Shops.Services;
using Shops.Tools;
using Shops.UI.Factory;
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
            while (_factory.CurrentMenu != null)
            {
                AnsiConsole.Clear();
                _factory.CurrentMenu.Show();
                _factory.CreateOrFindMenu(_factory.CurrentMenu.Choice);
            }
        }
    }
}