using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAuth.Models.DTO;
using AfyaSoftAuth.Models.DTO.Request;
using AfyaSoftAuth.Models.DTO.Response;
using AfyaSoftAuth.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AfyaSoftAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ResponseDTO  _responseDTO;
        private readonly IConfiguration _configuration;
        // private readonly IMessageBus _messageBus;
        // private readonly IMapper _mapper;
        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _responseDTO = new ResponseDTO();
            _configuration = configuration;
            // _messageBus = messageBus;
        }
        [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> Register(UserDTO userDTO)
        {
            try
            {
                var responseMessage = await _userService.RegisterUser(userDTO);
                if ( responseMessage == "User created successfully")
                {
                    _responseDTO.Message = responseMessage;
                    _responseDTO.Data = userDTO;
                    // send a message to the service bus
                    return Ok(_responseDTO);
                }
                else
                {
                    _responseDTO.Message = responseMessage;
                    _responseDTO.IsSuccess = false;
                    _responseDTO.Data = userDTO;

                    return BadRequest(_responseDTO);
                }
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = userDTO;

                return BadRequest(_responseDTO);
            }
        }
        [HttpPost("login")]
        public async Task<ActionResult<ResponseDTO>> Login(LoginRequestDto loginRequestDto)
        {
            try
            {
                var loginMessage = await _userService.LoginUser(loginRequestDto);
                _responseDTO.Message = "Success Login";
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = loginMessage;
                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = loginRequestDto;
                return BadRequest(_responseDTO);
            }
        }

        // get user by id
        [HttpGet("/{UserId}")]
        [Authorize]
        public async Task<ActionResult<ResponseDTO>> GetUserById(string UserId)
        {
            try
            {
                var response = await _userService.GetUserById(UserId);
                _responseDTO.Message = "Success";
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = response;
                return Ok(_responseDTO);            
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return Ok(_responseDTO);
            }
        }

        [HttpPost("role")]
        public async Task<ActionResult<ResponseDTO>> UpdateRole(UpdateUserRole updateUserRole)
        {
            try
            {
                var response = await _userService.UpdateUserRole(updateUserRole);
                _responseDTO.Message = response;
                _responseDTO.IsSuccess = true;
                _responseDTO.Data = null;
                return Ok(_responseDTO);            
            }
            catch(Exception ex){
                _responseDTO.Message = ex.Message;
                _responseDTO.IsSuccess = false;
                _responseDTO.Data = null;
                return Ok(_responseDTO);
            }
        }
    }
}