namespace Zaka.Core.API.Domain.Entities
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public Guid TransactionReference { get; set; }
        public string TransactionTypeCode { get; set; }
        public decimal Amount { get; set; }
        public string FromWallet { get; set; }
        public string ToWallet { get; set; }
        public string FromPhone { get; set; }
        public string ToPhone { get; set; }
        public string TransactionStatus { get; set; }
        public string VoucherNumber { get; set; }
    }
}