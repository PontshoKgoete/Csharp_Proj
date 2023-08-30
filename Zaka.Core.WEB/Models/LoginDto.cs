using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class LoginDto
    {
        public string? PhoneNumber { get; set; }
        public string? SecretKey { get; set; }
    }
}