

using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Wallet> Create(Wallet wallet);
        Task<Account> UpdateCredentials(string phoneNumber, string secretKeyHash);
        Task<Wallet> TopUpWallet(TopUpWalletDto wallet);
        Task<Wallet> SendMoney(SendMoneyDto sendMoney);
        Task<Wallet> Get(string phoneNumber);
        Task<IEnumerable<Account>> GetAll();
    }
}