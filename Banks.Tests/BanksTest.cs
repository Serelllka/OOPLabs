﻿using System;
using Banks.BuisnessLogic.Accounts;
using Banks.BuisnessLogic.Builders;
using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Models;
using Banks.BuisnessLogic.Services;
using Banks.BuisnessLogic.Tools;
using Banks.BuisnessLogic.ValueObject;
using Banks.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTest : System.IDisposable
    {
        private Context _context;
        private CentralBank _centralBank;
        private ClientBuilder _clientBuilder;
        
        [SetUp]
        public void Setup()
        {
            _context = new Context(
                new DbContextOptionsBuilder<Context>().
                    UseInMemoryDatabase(Guid.NewGuid().ToString()).
                    Options);

            var percentCalculator1 = new PercentCalculator();
            var percentCalculator2 = new PercentCalculator();
            var percentCalculator3 = new PercentCalculator();
            
            percentCalculator1.AddInterestRate(new InterestRate(1000, 3));
            percentCalculator1.AddInterestRate(new InterestRate(10000, 6));
            percentCalculator1.AddInterestRate(new InterestRate(100000, 11));
            
            percentCalculator2.AddInterestRate(new InterestRate(2000, 6));
            percentCalculator2.AddInterestRate(new InterestRate(20000, 9));
            percentCalculator2.AddInterestRate(new InterestRate(200000, 13));
            
            percentCalculator3.AddInterestRate(new InterestRate(3000, 3));
            percentCalculator3.AddInterestRate(new InterestRate(30000, 6));
            percentCalculator3.AddInterestRate(new InterestRate(300000, 11));
            
            _context.UpdateDatabase();
            var tmpCentralBank = new CentralBank(_context);
            _clientBuilder = new ClientBuilder();

            tmpCentralBank.RegisterNewBank(
                new Bank("Tinkoff", 20, 10, percentCalculator1));
            tmpCentralBank.RegisterNewBank(
                new Bank("AlphaBank", 15, 12, percentCalculator2));
            tmpCentralBank.RegisterNewBank(
                new Bank("Sberbank", 13, 9, percentCalculator3));
            
            _centralBank = new CentralBank(_context);
        }

        [Test]
        public void GetClientFromCentralBankLoadedFromDatabase_UserExists()
        {
            Assert.AreEqual(_centralBank.Banks.Count, 3);
            Assert.AreEqual(_centralBank.Clients.Count, 0);
        }

        [Test]
        public void AddsBank_BanksAmountIncreases()
        {
            var percentCalculator = new PercentCalculator();
            _centralBank.RegisterNewBank(new Bank("Esketit", 1000, 1000, percentCalculator));
            Assert.AreEqual(_centralBank.Banks.Count, 4);
        }

        [Test]
        public void TryAddClassesWithNullArguments_ThrowsException()
        {
            var percentCalculator = new PercentCalculator();
            const decimal creditTax = 100;
            const decimal debitPercent = 10;
            const string bankName = "testName";
            var testBank = new Bank(bankName, creditTax, debitPercent, percentCalculator);
            
            Assert.Catch<BanksException>(() =>
            {
                var bank = new Bank("Esketit", creditTax, debitPercent, null);
            });
            
            Assert.Catch<BanksException>(() =>
            {
                var bank = new Bank(null, creditTax, debitPercent, percentCalculator);
            });

            Assert.Catch<BanksException>(() =>
            {
                testBank.CreateNewCreditAccount(null);
            });
            
            Assert.Catch<BanksException>(() =>
            {
                testBank.CreateNewDebitAccount(null);
            });
            
            Assert.Catch<BanksException>(() =>
            {
                testBank.CreateNewDepositAccount(null);
            });
        }

        [Test]
        public void AddsAccountThenDelete_AccountsAmountIncreasesThenReduces()
        {
            var percentCalculator = new PercentCalculator();
            const decimal creditTax = 100;
            const decimal debitPercent = 10;
            const string bankName = "testName";
            var testBank = new Bank(bankName, creditTax, debitPercent, percentCalculator);
            _clientBuilder.SetClientName("Igor");
            _clientBuilder.SetClientSurname("NeNikolaev");
            _clientBuilder.SetPassport("2138439");
            Client client = _clientBuilder.Build();
            Account account1 = testBank.CreateNewCreditAccount(client);
            Account account2 = testBank.CreateNewDebitAccount(client);
            account1.AddMoney(10000);
            Assert.AreEqual(testBank.GetListOfClientsAccounts(client).Count, 2);

            Transaction transaction = _centralBank.MakeTransaction(account1, account2, 1000);
            Assert.AreEqual(account1.MoneyAmount, 9000);
        }

        [Test]
        public void ChangesAccountParameters_ClientsHasNewLog()
        {
            var percentCalculator = new PercentCalculator();
            const decimal creditTax = 100;
            const decimal debitPercent = 10;
            const string bankName = "testName";
            var testBank = new Bank(bankName, creditTax, debitPercent, percentCalculator);
            _clientBuilder.SetClientName("Igor");
            _clientBuilder.SetClientSurname("NeNikolaev");
            _clientBuilder.SetPassport("2138439");
            Client client = _clientBuilder.Build();
            Account account1 = testBank.CreateNewCreditAccount(client);
            testBank.ChangeCreditTax(12);
            Assert.AreEqual(client.UserLog.Count, 1);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}