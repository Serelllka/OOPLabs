using System;
using System.Collections.Generic;
using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Models;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Accounts
{
    public abstract class Account
    {
        protected Account(Bank bank, Client client)
        {
            MoneyAmount = 0;
            Id = Guid.NewGuid();
            OwnerBank = bank;
            OwnerClient = client;
        }

        protected Account()
        { }

        public Guid Id { get; private set; }
        public Bank OwnerBank { get; private set; }
        public Client OwnerClient { get; private set; }
        public decimal MoneyAmount { get; protected set; }

        public void AddMoney(decimal money)
        {
            if (money < 0)
            {
                throw new BanksException("You can't add a negative amount");
            }

            MoneyAmount += money;
        }

        public virtual void GetMoney(decimal money)
        {
            if (money < 0)
            {
                throw new BanksException("You can't withdraw a negative amount");
            }

            if (money > MoneyAmount)
            {
                throw new BanksException("The amount you want to withdraw is more than it contains");
            }

            MoneyAmount -= money;
        }

        public abstract bool CanWithdraw(decimal money);
        public abstract string GetTypeInString();
    }
}