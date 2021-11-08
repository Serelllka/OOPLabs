using System.Collections.Generic;
using Banks.BuisnessLogic.Entities;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class ClientMenu : Menu
    {
        private Client _client;
        private Bank _bank;
        public ClientMenu(Menu prevMenu, Bank bank, Client client)
            : base(prevMenu)
        {
            _client = client;
            _bank = bank;
        }

        public override void UpdateTable()
        {
            RenderTable = new Table();
            RenderTable.AddColumns(
                "Client Name",
                "Client Passport",
                "Client Id");
            RenderTable.AddRow(
                _client.Name + ' ' + _client.Surname,
                _client.PassportId,
                _client.Id.ToString());
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();

            SelectionOptions.Add(new ShowListOfAccountsCommand(
                this,
                _bank.GetListOfClientsAccounts(_client)));
            SelectionOptions.Add(new ExitCommand(this));
        }
    }
}