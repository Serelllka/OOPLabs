using System.Collections.Generic;
using Shops.Entities;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public abstract class Menu : IMenu
    {
        protected Menu(IMenu prevMenu, List<ShoppingListItem> shoppingList)
        {
            PrevMenu = prevMenu;
            SelectionOptions = new List<string>();
            ShoppingList = shoppingList;
            Table = new Table();
        }

        protected string Choice { get; private set; }
        protected IMenu PrevMenu { get; }

        protected List<string> SelectionOptions { get; set; }
        protected Table Table { get; set; }
        protected List<ShoppingListItem> ShoppingList { get; private set; }

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

        public abstract IMenu GenerateNextMenu();

        public abstract void UpdateTable();
    }
}