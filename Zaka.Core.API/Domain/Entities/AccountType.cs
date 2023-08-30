using System.ComponentModel.DataAnnotations;

namespace Zaka.Core.API.Domain.Entities
{
    public class AccountType
    {
        [Key]
        public int AccountId { get; set; }
        [MaxLength(100)]
        public string Type { get; set; }
    }
}