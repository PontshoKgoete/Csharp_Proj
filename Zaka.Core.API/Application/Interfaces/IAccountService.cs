using Zaka.Core.API.Application.Dtos;

namespace Zaka.Core.API.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ResponseDto> CreateAccount(CreateAccountDto account);
        Task<ResponseDto> GetAccount(string phoneNumber);
        Task<ResponseDto> UpdateCredentials(UpdateCredentialsDto updateCredentials);
        Task<ResponseDto> TopUpWallet(TopUpWalletDto topUpWallet);
        Task<ResponseDto> SendMoney(SendMoneyDto sendMoney);
        Task<LoginResponseDto> Login(LoginDto login);
    }
}