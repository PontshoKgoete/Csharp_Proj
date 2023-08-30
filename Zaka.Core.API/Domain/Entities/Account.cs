using Zaka.Core.API.Domain.Entities;

namespace Zaka.Core.API.Domain.Entities
{
    public class Account
    {
        public int AccountId { get; set; }
        public Guid AccountGuid { get; set; }
        public int AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string CellphoneNumber { get; set; }
        public string SecretKey { get; set; }
        public bool AccountVerified { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}