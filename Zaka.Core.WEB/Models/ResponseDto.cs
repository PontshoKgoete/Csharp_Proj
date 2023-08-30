using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zaka.Core.WEB.Models
{
    public class ResponseDto
    {
        public int? StatusCode { get; set; }
        public string? StatusDescription { get; set; }
        public string? Details { get; set; }
        public object? Data { get; set; }
    }
}