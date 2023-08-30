using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Zaka.Core.API.Application.Dtos;
using Zaka.Core.API.Application.Interfaces;
using Zaka.Core.API.Domain.Entities;
using Zaka.Core.API.Persistence.Repositories;

namespace Zaka.Core.API.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AccountService(IMapper mapper, IConfiguration config)
        {
            _config = config;
            _mapper = mapper;
            _accountRepository = new AccountRepository();
        }

        public async Task<ResponseDto> CreateAccount(CreateAccountDto account)
        {
            ResponseDto response = new();
            try
            {
                var newAccount = _mapper.Map<Account>(account);
                newAccount.CreatedDate = DateTime.UtcNow;
                newAccount.AccountGuid = Guid.NewGuid();
                newAccount.SecretKey = BCrypt.Net.BCrypt.HashPassword(account.SecretKey);

                var wallet = new Wallet
                {
                    WalletNumber = GenerateWalletNumber(),
                    Account = newAccount
                };

                response.Data = await _accountRepository.Create(wallet);
                response.StatusCode = 201;
                response.StatusDescription = "Account Created";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Details = "Bad Request";
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> GetAccount(string phoneNumber)
        {
            ResponseDto response = new();
            try
            {
                response.Data = await _accountRepository.Get(phoneNumber);
                response.StatusCode = 200;
                response.StatusDescription = "OK";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Details = "Bad Request";
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> UpdateCredentials(UpdateCredentialsDto updateCredentials)
        {
            ResponseDto response = new();
            try
            {
                var hashSecretKey = BCrypt.Net.BCrypt.HashPassword(updateCredentials.SecretKey);
                response.Data = await _accountRepository.UpdateCredentials(updateCredentials.PhoneNumber, hashSecretKey);
                response.StatusCode = 200;
                response.StatusDescription = "Credentials Updated.";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.Details = "Bad Request";
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<LoginResponseDto> Login(LoginDto login)
        {
            LoginResponseDto response = new();
            try
            {
                var account = await _accountRepository.Get(login.PhoneNumber);
                if (account is null)
                    throw new Exception("Invalid credentials.");

                // string secretKeyHash = BCrypt.Net.BCrypt.HashPassword(login.SecretKey);
                bool verified = BCrypt.Net.BCrypt.Verify(login.SecretKey, account.Account?.SecretKey);
                if (!verified)
                    throw new Exception("Invalid credentials.");

                response = GenerateAuthToken(account);
                response.StatusCode = 200;
                response.StatusDescription = "Login Success.";

            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Login Failure.";
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> TopUpWallet(TopUpWalletDto topUpWallet)
        {
            ResponseDto response = new();
            try
            {
                response.Data = await _accountRepository.TopUpWallet(topUpWallet);
                response.StatusCode = 200;
                response.StatusDescription = "Wallet Topped-Up";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Bad Request";
                response.Details = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> SendMoney(SendMoneyDto sendMoney)
        {
            ResponseDto response = new();
            try
            {
                sendMoney.VoucherNumber = GenerateVoucherNumber();
                response.Data = await _accountRepository.SendMoney(sendMoney);
                response.StatusCode = 200;
                response.StatusDescription = "Sent Money";
            }
            catch (Exception ex)
            {
                response.StatusCode = 400;
                response.StatusDescription = "Bad Request";
                response.Details = ex.Message;
            }

            return response;
        }

        private string GenerateVoucherNumber()
        {
            var random = new Random(); // TODO: CONFIRM WITH THE FUNCTIONAL SPEC HOW MANY DIGIT SHOULD BE
            var date = DateTime.Now.ToString("yyyyMMdd");
            return $"ZV-{date}{random.Next(0, 100000)}";
        }

        private string GenerateWalletNumber()
        {
            //TODO: CHECK IF WALLET NUMBER IS ALREADY TAKEN BEFORE CREATING A WALLET
            var random = new Random();
            var date = DateTime.Now.ToString("yyyyMMdd");
            return $"{date}{random.Next(0, 100000)}";
        }

        private LoginResponseDto GenerateAuthToken(Wallet wallet)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var expires = DateTime.Now.AddMinutes(30);
            var token = new JwtSecurityToken(
                issuer: _config.GetSection("AppSettings:Issurer").Value,
                audience: _config.GetSection("AppSettings:Audience").Value,
                claims: GetClaims(wallet),
                expires: expires,
                signingCredentials: signingCredentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return new LoginResponseDto
            {
                AccessToken = tokenHandler.WriteToken(token),
                ValidFrom = expires.AddMinutes(-30).ToString(),
                ValidTo = expires.ToString()
            };
        }

        private Claim[] GetClaims(Wallet wallet)
        {
            var accountClaims = new List<Claim>();
            accountClaims.Add(new Claim("accountGuid", wallet.Account?.AccountGuid.ToString()));
            accountClaims.Add(new Claim("accountType", wallet.Account?.AccountType.Type));
            accountClaims.Add(new Claim("accountVerified", wallet.Account?.AccountVerified.ToString()));
            accountClaims.Add(new Claim("username", wallet.Account?.Username));
            accountClaims.Add(new Claim("fullName", $"{wallet.Account?.FirstName} {wallet.Account.LastName}"));
            accountClaims.Add(new Claim("phoneNumber", wallet.Account?.CellphoneNumber));
            return accountClaims.ToArray();
        }


    }
}