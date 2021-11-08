namespace Banks.UI.Commands
{
    public class AddMoneyToAccountCommand : Command
    {
        public AddMoneyToAccountCommand(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "Add money to account";
        }
    }
}