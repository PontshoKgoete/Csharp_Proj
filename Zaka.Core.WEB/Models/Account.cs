using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? StatusDescription { get; set; }
        public object? Details { get; set; }
        public WalletData? Data { get; set; }
    }

    public class WalletData
    {
        public int WalletId { get; set; }
        public int AccountId { get; set; }
        public Account? Account { get; set; }
        public string? WalletNumber { get; set; }
        public decimal availableMoney { get; set; }
        public decimal TopUpMoney { get; set; }
        public DateTime TopUpDate { get; set; }
    }

    public class Account
    {
        public int AccountId { get; set; }
        public string? AccountGuid { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType? AccountType { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? Username { get; set; }
        public string? CellphoneNumber { get; set; }
        public string? SecretKey { get; set; }
        public bool AccountVerified { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class AccountType
    {
        public int AccountId { get; set; }
        public string? Type { get; set; }
    }

}