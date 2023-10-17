using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class OrderDetailsDto
    {
        public Guid OrderDetailsId { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid AppointmentId { get; set; }

        public AppointmentDTO? appointment { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
    }
}