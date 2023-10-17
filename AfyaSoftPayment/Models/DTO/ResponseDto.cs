using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class ResponseDto
    {
        public object? Data { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }
}
