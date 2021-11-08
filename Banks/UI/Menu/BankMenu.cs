using System.Collections.Generic;
using System.Net.NetworkInformation;
using Banks.BusinessLogic.Entities;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class BankMenu : Menu
    {
        private Bank _bank;

        public BankMenu(Menu prevMenu, Bank bank)
            : base(prevMenu)
        {
            _bank = bank;
        }

        public override void UpdateTable()
        {
            RenderTable = new Table();
            RenderTable.AddColumns("N", "Client Name", "Client Passport", "Client Id");

            int index = 1;
            foreach (Client client in _bank.Clients)
            {
                RenderTable.AddRow(
                    index.ToString(),
                    client.Name + ' ' + client.Surname,
                    client.PassportId,
                    client.Id.ToString());
            }
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();

            foreach (Client client in _bank.Clients)
            {
                SelectionOptions.Add(new SelectClientCommand(this, _bank, client));
            }

            SelectionOptions.Add(new ExitCommand(this));
            if (SelectionOptions.Count == 1)
            {
                SelectionOptions.Add(new NullCommand(this));
            }
        }
    }
}