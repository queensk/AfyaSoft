using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftPayment.Models.DTO;

namespace AfyaSoftPayment.Services.Iservice
{
    public interface IOrderService
    {
        Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto);


        Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto);

        Task<bool> ValidatePayment(Guid OrderId);

        Task<IEnumerable<OrderHeaderDto>> Getorders(string? UserId="");

        Task<OrderHeaderDto> Getorder(Guid id);

        Task<OrderHeaderDto> updateOrder(Guid orderId, string newStatus);
    }
}