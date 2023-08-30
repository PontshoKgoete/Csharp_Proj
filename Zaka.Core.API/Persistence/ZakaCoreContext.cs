using Microsoft.EntityFrameworkCore;
using Zaka.Core.API.Domain.Entities;
using Zaka.Core.API.Persistence.Configurations;

namespace Zaka.Core.API.Persistence
{
    public class ZakaCoreContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            IConfiguration config = new ConfigurationBuilder()
                                        .AddJsonFile($"appsettings.{environment}.json")
                                        .AddEnvironmentVariables()
                                        .Build();
            optionsBuilder.UseNpgsql(config.GetConnectionString("ZakaCoreDbConnection"));

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountType> AccountTypes { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfigurations());
            modelBuilder.ApplyConfiguration(new WalletConfigurations());
            modelBuilder.ApplyConfiguration(new TransactionConfigurations());

            modelBuilder.Entity<AccountType>()
            .HasData(
                new AccountType { AccountId = 1, Type = "Individual" },
                new AccountType { AccountId = 2, Type = "Business" }
            );
        }

    }
}