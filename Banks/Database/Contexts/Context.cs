using Banks.BuisnessLogic.Accounts;
using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Models;
using Banks.BuisnessLogic.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace Banks.Database.Contexts
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CreditAccount> CreditAccounts { get; set; }
        public DbSet<DebitAccount> DebitAccounts { get; set; }
        public DbSet<DepositAccount> DepositAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public void UpdateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public void LoadData()
        {
            Transactions.Load();
            Clients.Load();
            Accounts.Load();
            Banks.Load();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PercentCalculator>().HasMany<InterestRate>("_interestRate");
            modelBuilder.Entity<Client>().HasMany<SubsriptionInfo>("_subscriptionInfo");
            modelBuilder.Entity<Bank>().HasMany<Account>("_accounts")
                .WithOne(x => x.OwnerBank);
            modelBuilder.Entity<Bank>().HasMany<Client>("_clients");
        }
    }
}