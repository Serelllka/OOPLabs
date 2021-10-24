namespace Shops.UI.Commands
{
    public class CommandExit : Command
    {
        public CommandExit(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            return null;
        }
    }
}