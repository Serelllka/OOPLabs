using System;

namespace Banks.UI.Commands
{
    public abstract class Command
    {
        protected Command(Menu.Menu currentMenu)
        {
            CurrentMenu = currentMenu;
        }

        protected Menu.Menu CurrentMenu { get; set; }

        public abstract Menu.Menu Execute();

        public abstract override string ToString();
    }
}