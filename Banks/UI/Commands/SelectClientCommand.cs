using Banks.BusinessLogic.Entities;
using Banks.UI.Menu;

namespace Banks.UI.Commands
{
    public class SelectClientCommand : Command
    {
        private Client _client;
        private Bank _bank;
        public SelectClientCommand(Menu.Menu currentMenu, Bank bank, Client client)
            : base(currentMenu)
        {
            _bank = bank;
            _client = client;
        }

        public override Menu.Menu Execute()
        {
            return new ClientMenu(CurrentMenu, _bank, _client);
        }

        public override string ToString()
        {
            return _client.Name;
        }
    }
}