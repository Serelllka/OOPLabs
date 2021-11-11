namespace Banks.UI.Commands
{
    public class MoreInfoAboutAccountCommand : Command
    {
        public MoreInfoAboutAccountCommand(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            return CurrentMenu;
        }

        public override string ToString()
        {
            return "Get transaction info";
        }
    }
}