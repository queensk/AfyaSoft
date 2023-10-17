using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftPayment.Models.DTO;
using AfyaSoftPayment.Services.Iservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfyaSoftPayment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        
        private readonly IOrderService _orderService;
        private readonly ResponseDto _responseDto;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
            _responseDto = new ResponseDto();
        }

        [HttpGet("Getorders")]
        [Authorize]
        public async Task <ActionResult<ResponseDto>> Get(string? userId)
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    var res = await _orderService.Getorders();
                    _responseDto.Data=res;
                }
                else
                {
                    var response = await _orderService.Getorders(userId);
                    _responseDto.Data=response;
                }
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPut("Getorders")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> updateOrder(Guid orderId , string orderStatus)
        {
            try
            {
                    var res = await _orderService.updateOrder(orderId,orderStatus);
                    _responseDto.Data = res; 
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpGet("Getorder/id")]
        [Authorize]
        public async Task<ActionResult<ResponseDto>> Getorder(Guid id)
        {
            try
            {
                var response = await _orderService.Getorder(id);
                _responseDto.Data=response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> AddOrder(CartDto cartDto)
        {
            try
            {
               var response =await _orderService.CreateOrderHeader(cartDto);
                _responseDto.Data=response;   
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }

        [HttpPost("StripePayment")]
        public async Task<ActionResult<ResponseDto>> StripePayment(StripeRequestDto stripeRequestDto)
        {
            try
            {
                var response = await _orderService.StripePayment(stripeRequestDto);
                _responseDto.Data = response;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }


        [HttpPost("validatePayment")]
        public async Task<ActionResult<ResponseDto>> ValidatePayment(Guid orderId)
        {
            try
            {
                var response = await _orderService.ValidatePayment(orderId);
                _responseDto.Data = response;

                
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = ex.InnerException.Message;
                return BadRequest(_responseDto);
            }
            return Ok(_responseDto);
        }
    }
}