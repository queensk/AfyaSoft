using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class CartDetailsDto
    {
        public Guid CartDetailsId { get; set; }
        public Guid CartHeaderId { get; set; }
        public CartHeaderDto? CartHeaderDto { get; set; }
        public Guid ProductId { get; set; }
        public AppointmentDTO? appointment { get; set; }
        public int Count { get; set; }
    }
}