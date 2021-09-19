using System.Collections.Generic;
using System.Data;
using Spectre.Console;

namespace Shops.UI.Menu
{
    public abstract class Menu
    {
        protected Menu(IMenu prevMenu)
        {
            PrevMenu = prevMenu;
            SelectionOptions = new List<string>();
            Table = new Table();
        }

        protected string Choice { get; private set; }
        protected IMenu PrevMenu { get; }

        protected List<string> SelectionOptions { get; set; }
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