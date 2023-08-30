using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class CreateAccountDto
    {
        public int AccountTypeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? CellphoneNumber { get; set; }
        public string? SecretKey { get; set; }
    }
}