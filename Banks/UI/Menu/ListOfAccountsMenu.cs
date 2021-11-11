using System.Collections.Generic;
using Banks.BusinessLogic.Accounts;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class ListOfAccountsMenu : Menu
    {
        private IReadOnlyList<Account> _accounts;

        public ListOfAccountsMenu(Menu prevMenu, IReadOnlyList<Account> accounts)
            : base(prevMenu)
        {
            _accounts = accounts;
        }

        public override void UpdateTable()
        {
            RenderTable = new Table();
            RenderTable.AddColumns("N", "Client Name", "Client Passport", "Account Type", "Account Id");
            int index = 1;
            foreach (Account account in _accounts)
            {
                string accountType = account switch
                {
                    CreditAccount => "credit",
                    DebitAccount => "debit",
                    _ => "deposit"
                };
                RenderTable.AddRow(
                    index.ToString(),
                    account.OwnerClient.Name + ' ' + account.OwnerClient.Surname,
                    account.OwnerClient.PassportId,
                    accountType,
                    account.Id.ToString());
                ++index;
            }
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();
            foreach (Account account in _accounts)
            {
                SelectionOptions.Add(new SelectAccountCommand(this, account));
            }

            SelectionOptions.Add(new ExitCommand(this));
            if (SelectionOptions.Count == 1)
            {
                SelectionOptions.Add(new NullCommand(this));
            }
        }
    }
}