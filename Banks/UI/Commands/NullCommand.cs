using System;

namespace Banks.UI.Commands
{
    public class NullCommand : Command
    {
        public NullCommand(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override string ToString()
        {
            return "-";
        }

        public override Menu.Menu Execute()
        {
            return CurrentMenu;
        }
    }
}