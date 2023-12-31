using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AfyaSoftPayment.Models
{
    public class OrderHeader
    {
        [Key]
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

        public OrderDetails OrderDetails { get; set; }
    }
}