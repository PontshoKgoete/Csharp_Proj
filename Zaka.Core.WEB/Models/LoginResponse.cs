using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Details { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
    }
}