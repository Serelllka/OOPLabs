using System.Collections.Generic;
using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Services;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class ListOfBanksMenu : Menu
    {
        private CentralBank _centralBank;
        public ListOfBanksMenu(
            Menu prevMenu,
            CentralBank centralBank)
            : base(prevMenu)
        {
            _centralBank = centralBank;
        }

        public override void UpdateTable()
        {
            RenderTable = new Table();
            RenderTable.AddColumns("N", "Bank Name", "Bank Id");
            int index = 1;
            foreach (Bank bank in _centralBank.Banks)
            {
                RenderTable.AddRow(index.ToString(), bank.Name, bank.Id.ToString());
                index++;
            }
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();

            foreach (Bank bank in _centralBank.Banks)
            {
                SelectionOptions.Add(new SelectBankCommand(this, bank));
            }

            SelectionOptions.Add(new ExitCommand(this));
        }
    }
}