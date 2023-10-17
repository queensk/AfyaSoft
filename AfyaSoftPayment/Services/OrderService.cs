using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftPayment.Data;
using AfyaSoftPayment.Models;
using AfyaSoftPayment.Models.DTO;
using AfyaSoftPayment.Services.Iservice;
using AfySoftMessageBus.MessageBus;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace AfyaSoftPayment.Services
{
    public class OrderService : IOrderService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IMessageBus _messageBus;
        // private readonly IMessage _messageService;
        // private readonly IRabbitMQPublisher _rabbitMQPublisher;
        public OrderService(IMapper mapper, AppDbContext  appDbContext, IMessageBus messsageBus)
        {
            _context = appDbContext;
            _mapper = mapper;
            _messageBus = messsageBus;
            // _messageService=message;
            // _rabbitMQPublisher = rabbitMQPublisher; 
        }
        public async Task<OrderHeaderDto> CreateOrderHeader(CartDto cartDto)
        {
             OrderHeaderDto orderHeaderDto = _mapper.Map<OrderHeaderDto>(cartDto.CartHeader);
            orderHeaderDto.Status = "Pending";
            orderHeaderDto.OrderDetails = _mapper.Map<OrderDetailsDto>(cartDto.CartDetails);
            orderHeaderDto.OrderTotal= Math.Round(orderHeaderDto.OrderTotal,2);
            OrderHeader newOrder = _mapper.Map<OrderHeader>(orderHeaderDto);
            //await Console.Out.WriteLineAsync(newOrder.UserId);
            var item =_context.OrderHeaders.Add(newOrder).Entity;
            await _context.SaveChangesAsync();


            orderHeaderDto.OrderHeaderId= item.OrderHeaderId;
            return orderHeaderDto;
        }

        public async Task<OrderHeaderDto> Getorder(Guid id)
        {
           OrderHeader orderHeader = await _context.OrderHeaders.Include(x=>x.OrderDetails).FirstAsync(o=>o.OrderHeaderId==id);
            return _mapper.Map<OrderHeaderDto>(orderHeader);
        }

        public async Task<IEnumerable<OrderHeaderDto>> Getorders(string? UserId)
        {
            if (UserId == null || string.IsNullOrWhiteSpace(UserId))
            {
                IEnumerable<OrderHeader> orders = await _context.OrderHeaders.Include(x => x.OrderDetails).OrderByDescending(u => u.OrderHeaderId).ToListAsync();
                return _mapper.Map<IEnumerable<OrderHeaderDto>>(orders);
            }
            IEnumerable<OrderHeader> orders1 = await _context.OrderHeaders.Include(x => x.OrderDetails).Where(x=>x.UserId==UserId).OrderByDescending(u => u.OrderHeaderId).ToListAsync();
            return _mapper.Map<IEnumerable<OrderHeaderDto>>(orders1);
        }

        public async Task<StripeRequestDto> StripePayment(StripeRequestDto stripeRequestDto)
        {
            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };


            var sessionLineItem = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)(stripeRequestDto.OrderHeader.OrderDetails.Price * 100),
                    Currency = "kes",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = stripeRequestDto.OrderHeader.OrderDetails.ProductName
                    },


                },
                Quantity = stripeRequestDto.OrderHeader.OrderDetails.Count
            };
            options.LineItems.Add(sessionLineItem);

            var DiscountObj = new List<SessionDiscountOptions>()
           {
               new SessionDiscountOptions()
               {
                   Coupon= stripeRequestDto.OrderHeader.CouponCode
               }
           };

            if (stripeRequestDto.OrderHeader.Discount > 0)
            {
                options.Discounts = DiscountObj;
            }

            var service = new SessionService();
            Session session= service.Create(options);

            //URL
            //Session ID- portion of the URL
            stripeRequestDto.StripeSessionId = session.Id;
            stripeRequestDto.StripeSessionUrl = session.Url;

            OrderHeader order = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.OrderHeaderId == stripeRequestDto.OrderHeader.OrderHeaderId);
            order.Name = stripeRequestDto.OrderHeader.Name;
            order.Email= stripeRequestDto.OrderHeader.Email;
            order.Phone = stripeRequestDto.OrderHeader.Phone;   

            order.StripeSessionId = session.Id;
            await _context.SaveChangesAsync();

            return stripeRequestDto;

        }

        public async Task<OrderHeaderDto> updateOrder(Guid orderId, string newStatus)
        {
            OrderHeader orderHeader = await _context.OrderHeaders.FirstAsync(u => u.OrderHeaderId == orderId);
            try
            {
           

                if (orderHeader != null)
                {
                    if (!string.IsNullOrWhiteSpace(orderHeader.Phone) && newStatus== "ReadyForPickup")
                    {

                        // _messageService.sendMessage(orderHeader.Phone);
                    }

                    if (newStatus == "Cancelled")
                    {
                        var options = new RefundCreateOptions()
                        {
                            Reason = RefundReasons.RequestedByCustomer,
                            PaymentIntent = orderHeader.PaymentIntentId
                        };

                        var service = new RefundService();
                        Refund refund = service.Create(options);
                    }

                    orderHeader.Status = newStatus;
                    await _context.SaveChangesAsync();
                }
               
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
            return _mapper.Map<OrderHeaderDto>(orderHeader);
        }

        public async  Task<bool> ValidatePayment(Guid OrderId)
        {
            OrderHeader order = await _context.OrderHeaders.FirstOrDefaultAsync(x => x.OrderHeaderId == OrderId);

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            var paymentIntentService = new PaymentIntentService();
            var id = session.PaymentIntentId;
            if(id==null)
            {
                return false;
            }
            PaymentIntent paymentInt = paymentIntentService.Get(id);

            if (paymentInt.Status == "succeeded")
            {
                order.PaymentIntentId = paymentInt.Id;
                order.Status = "Approved";
                await  _context.SaveChangesAsync();
                // var rewards = new RewardsDto()
                // {
                //     Email = order.Email,
                //     TotalAmount = (int)order.OrderTotal,
                //     UserId = order.UserId

                // };
                //Communicate with Rewards Topic
                // await _messageBus.PublishMessage(rewards, "ordertopic");
                // _rabbitMQPublisher.PublishMessage(rewards, "ordertopic");
                return true;
                
            }
            return false;

        }
    }
}