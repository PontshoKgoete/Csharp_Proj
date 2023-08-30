using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Application.Interfaces;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Persistence.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        public async Task<IEnumerable<Account>> GetAll()
        {
            using (var context = new ZakaCoreContext())
            {
                return await context.Accounts
                                    .Include(a => a.AccountType).ToListAsync();
            }

        }

        public async Task<Wallet> Get(string phoneNumber)
        {
            using (var context = new ZakaCoreContext())
            {
                return await context.Wallets
                    .Include(w => w.Account)
                        .ThenInclude(at => at.AccountType)
                    .FirstOrDefaultAsync(a => a.Account.CellphoneNumber.Equals(phoneNumber));
            }

        }

        public async Task<Account> UpdateCredentials(string phoneNumber, string secretKeyHash)
        {
            using (var context = new ZakaCoreContext())
            {
                var account = await context.Accounts
                              .FirstOrDefaultAsync(a => a.CellphoneNumber.Equals(phoneNumber));
                account.SecretKey = secretKeyHash;
                await context.SaveChangesAsync();

                return account;
            }

        }

        public async Task<Wallet> Create(Wallet wallet)
        {
            using (var context = new ZakaCoreContext())
            {
                await context.AddAsync(wallet);
                await context.SaveChangesAsync();
                return wallet;
            }
        }

        public async Task<Wallet> TopUpWallet(TopUpWalletDto topUpWallet)
        {
            using (var context = new ZakaCoreContext())
            {
                return await TopUpWallet(context, topUpWallet.WalletNumber, topUpWallet.TopUpMoney);
            }

        }

        public async Task<Wallet> SendMoney(SendMoneyDto sendMoney)
        {
            using (var context = new ZakaCoreContext())
            {
                using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
                {
                    var senderWallet = await WithdrawFromWallet(context, sendMoney.FromWallet, sendMoney.Amount);

                    if (sendMoney.IsSendingToPersonWithZakaApp)
                        await TopUpWallet(context, sendMoney.ToWallet, sendMoney.Amount);

                    await CreateTransaction(context, sendMoney);
                    await transaction.CommitAsync();

                    return senderWallet;
                }
            }

        }

        private async Task CreateTransaction(ZakaCoreContext context, SendMoneyDto sendMoney)
        {
            var newTransaction = new Transaction
            {
                Amount = sendMoney.Amount,
                TransactionReference = Guid.NewGuid(),
                TransactionTypeCode = "SEND_MONEY", //TODO: CENTRALIZE TRANSACTIONCODE
                VoucherNumber = sendMoney.VoucherNumber,
            };

            if (sendMoney.IsSendingToPersonWithZakaApp)
            {
                newTransaction.FromWallet = sendMoney.FromWallet;
                newTransaction.ToWallet = sendMoney.ToWallet;
                newTransaction.TransactionStatus = "TRANSACTION_COMPLETED"; //TODO: CENTRALIZE STATUSES
            }
            else
            {
                newTransaction.FromPhone = sendMoney.FromPhone;
                newTransaction.ToPhone = sendMoney.ToPhone;
                newTransaction.TransactionStatus = "NEW_TRANSACTION";
            }

            await context.Transactions.AddAsync(newTransaction);
            await context.SaveChangesAsync();
        }

        private async Task<Wallet> WithdrawFromWallet(ZakaCoreContext context, string wallet, decimal amount)
        {
            var walletToUpdate = await context.Wallets
                                   .FirstOrDefaultAsync(w => w.WalletNumber.Equals(wallet));
            if (walletToUpdate is null)
                throw new Exception($"Wallet: {wallet} not found.");

            walletToUpdate.AvailableMoney -= amount;
            await context.SaveChangesAsync();

            return walletToUpdate;
        }

        private async Task<Wallet> TopUpWallet(ZakaCoreContext context, string wallet, decimal amount)
        {
            var walletToUpdate = await context.Wallets
                                    .FirstOrDefaultAsync(w => w.WalletNumber.Equals(wallet));
            if (walletToUpdate is null)
                throw new Exception($"Wallet: {wallet} not found.");

            walletToUpdate.AvailableMoney += amount;
            walletToUpdate.TopUpMoney = amount;
            walletToUpdate.TopUpDate = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return walletToUpdate;
        }
    }
}