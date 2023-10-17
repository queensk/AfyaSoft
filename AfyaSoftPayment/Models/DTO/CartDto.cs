using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class CartDto
    {
        public CartHeaderDto CartHeader { get; set; }   

        public CartDetailsDto? CartDetails { get; set; }
    }
}