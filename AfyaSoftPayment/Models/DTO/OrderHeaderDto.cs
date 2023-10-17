using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models.DTO
{
    public class OrderHeaderDto
    {
        public Guid OrderHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? CouponCode { get; set; }
        public double Discount { get; set; }
        public double OrderTotal { get; set; }

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public string? Status { get; set; }

        public string? PaymentIntentId { get; set; }
        public string? StripeSessionId { get; set; }

        public OrderDetailsDto OrderDetails { get; set; }
    }
}