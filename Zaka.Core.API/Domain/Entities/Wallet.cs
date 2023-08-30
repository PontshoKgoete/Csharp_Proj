namespace Zaka.Core.API.Domain.Entities
{
    public class Wallet
    {
        public int WalletId { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public string WalletNumber { get; set; }
        public decimal AvailableMoney { get; set; }
        public decimal TopUpMoney { get; set; }
        public DateTime TopUpDate { get; set; }
    }
}