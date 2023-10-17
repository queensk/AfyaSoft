using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftPayment.Models.DTO;

namespace AfyaSoftPayment.Models
{
    public class OrderDetails
    {
        [Key]
        public Guid OrderDetailsId { get; set; }
        public Guid OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        public OrderHeader OrderHeader { get; set; }
        public Guid AppointmentId { get; set; }
        [NotMapped]
        public AppointmentDTO? Appointment { get; set; }
        public int Count { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }
    
    }
}