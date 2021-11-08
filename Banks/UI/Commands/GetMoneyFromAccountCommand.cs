namespace Banks.UI.Commands
{
    public class GetMoneyFromAccountCommand : Command
    {
        public GetMoneyFromAccountCommand(Menu.Menu currentMenu)
            : base(currentMenu)
        {
        }

        public override Menu.Menu Execute()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "Get money from account";
        }
    }
}