﻿using System.Collections.Generic;
using System.Globalization;
using Banks.BuisnessLogic.Accounts;
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
            RenderTable = new Table();
            RenderTable.AddColumns("Owner name", "Owner bank", "Money amount", "Account type");
            RenderTable.AddRow(
                _account.OwnerClient.Name + ' ' + _account.OwnerClient.Surname,
                _account.OwnerBank.Name,
                _account.MoneyAmount.ToString(CultureInfo.InvariantCulture),
                _account.GetTypeInString());
        }

        public override void UpdateListOfOptions()
        {
            SelectionOptions = new List<Command>();

            if (_account.CanWithdraw(0))
            {
                SelectionOptions.Add(new GetMoneyFromAccountCommand(this));
            }

            SelectionOptions.Add(new AddMoneyToAccountCommand(this));
            SelectionOptions.Add(new MoreInfoAboutAccountCommand(this));
            SelectionOptions.Add(new ExitCommand(this));
        }
    }
}