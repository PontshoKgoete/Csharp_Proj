using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class ValidateOtp
    {
        public string? PhoneNumber { get; set; }
        public string? Otp { get; set; }
    }
}