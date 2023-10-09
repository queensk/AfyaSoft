using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftAuth.Models.DTO.Response
{
    public class UserResponseDTO
    {
        public string Id { get; set; }=string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }
}