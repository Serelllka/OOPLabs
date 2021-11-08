using System.Collections.Generic;
using Banks.BuisnessLogic.Services;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class CentralBankMenu : Menu
    {
        private CentralBank _centralBank;
        public CentralBankMenu(Menu prevMenu, CentralBank centralBank)
            : base(prevMenu)
        {
            _centralBank = centralBank;
        }

        public override void UpdateTable()
        {
            RenderTable = new Table();
            RenderTable.AddColumns("Central Bank");
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();
            SelectionOptions.Add(new ShowListOfBanksCommand(this, _centralBank));
            SelectionOptions.Add(new CreateBankCommand(this, _centralBank));
        }
    }
}