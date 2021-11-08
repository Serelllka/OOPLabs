namespace Banks.UI.Commands
{
    public class ExitCommand : Command
    {
        public ExitCommand(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            return CurrentMenu.PrevMenu;
        }

        public override string ToString()
        {
            return "Exit";
        }
    }
}