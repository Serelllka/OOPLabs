using System.Collections.Generic;
using System.Globalization;
using Banks.BusinessLogic.Accounts;
using Banks.UI.Commands;
using Spectre.Console;

namespace Banks.UI.Menu
{
    public class AccountMenu : Menu
    {
        private Account _account;
        public AccountMenu(Menu prevMenu, Account account)
            : base(prevMenu)
        {
            _account = account;
        }

        public override void UpdateTable()
        {
            string accountType = _account switch
            {
                CreditAccount => "credit",
                DebitAccount => "debit",
                _ => "deposit"
            };

            RenderTable = new Table();
            RenderTable.AddColumns("Owner name", "Owner bank", "Money amount", "Account type");
            RenderTable.AddRow(
                _account.OwnerClient.Name + ' ' + _account.OwnerClient.Surname,
                _account.OwnerBank.Name,
                _account.MoneyAmount.ToString(CultureInfo.InvariantCulture),
                accountType);
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();

            if (_account.CanWithdraw(0))
            {
                SelectionOptions.Add(new GetMoneyFromAccountCommand(this));
            }

            SelectionOptions.Add(new AddMoneyToAccountCommand(this, _account));
            SelectionOptions.Add(new MoreInfoAboutAccountCommand(this));
            SelectionOptions.Add(new ExitCommand(this));
            if (SelectionOptions.Count == 1)
            {
                SelectionOptions.Add(new NullCommand(this));
            }
        }
    }
}