using System.Collections.Generic;
using Shops.Entities;
using Shops.UI.Models;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public abstract class Menu : IMenu
    {
        private ClientContext _context;
        protected Menu(Menu prevMenu, List<ShoppingListItem> shoppingList, ClientContext context)
        {
            PrevMenu = prevMenu;
            SelectionOptions = new List<string>();
            ShoppingList = shoppingList;
            _context = context;
            Table = new Table();
        }

        public ClientContext Context => _context;
        public Menu PrevMenu { get; }
        public string Choice { get; private set; }
        public List<string> SelectionOptions { get; set; }
        public List<ShoppingListItem> ShoppingList { get; private set; }
        protected Table Table { get; set; }

        public void Show()
        {
            UpdateTable();
            if (Table.Columns.Count > 0)
            {
                AnsiConsole.Render(Table);
            }

            Choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .AddChoices(SelectionOptions));
        }

        public abstract void UpdateTable();
    }
}