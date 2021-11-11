using System;
using System.Collections.Generic;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public abstract class Menu
    {
        private Menu _prevMenu;

        protected Menu(Menu prevMenu)
        {
            _prevMenu = prevMenu;
            RenderTable = new Table();
        }

        public Menu PrevMenu => _prevMenu;
        public string Choice { get; private set; }
        protected Table RenderTable { get; set; }
        protected List<Command> SelectionOptions { get; set; }

        public Command Show()
        {
            UpdateTable();
            UpdateListOfOptions();
            if (RenderTable.Columns.Count > 0)
            {
                AnsiConsole.Write(RenderTable);
            }

            return AnsiConsole.Prompt(
                new SelectionPrompt<Command>()
                    .PageSize(10)
                    .AddChoices(SelectionOptions));
        }

        public abstract void UpdateTable();
        public abstract void UpdateListOfOptions();
    }
}