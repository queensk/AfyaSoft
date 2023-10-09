using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftAuth.Models.DTO.Request
{
    public class RegisterRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}