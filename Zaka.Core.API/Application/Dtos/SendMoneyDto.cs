namespace Zaka.Core.API.Application.Dtos
{
    public class SendMoneyDto
    {
        public string? FromPhone { get; set; }
        public string? FromWallet { get; set; }
        public string? ToPhone { get; set; }
        public string? ToWallet { get; set; }
        public decimal Amount { get; set; }
        public string VoucherNumber { get; set; }
        public string? Message { get; set; }
        public bool IsSendingToPersonWithZakaApp { get; set; }
    }
}