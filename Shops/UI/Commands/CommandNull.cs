namespace Shops.UI.Commands
{
    public class CommandNull : Command
    {
        public CommandNull(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            return CurrentMenu;
        }
    }
}