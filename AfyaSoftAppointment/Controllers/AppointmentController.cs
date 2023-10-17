using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAppointment.Model;
using AfyaSoftAppointment.Model.DTO;
using AfyaSoftAppointment.Services.IService;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AfyaSoftAppointment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAppointmentService _appointmentService;
        private readonly ResponseDto _responseDto;
        public AppointmentController(IMapper mapper, IAppointmentService appointmentService, ResponseDto responseDto)
        {
            _mapper = mapper;
            _appointmentService = appointmentService;
            _responseDto = responseDto;
        }
        // Get appointments
        [HttpGet]
        public async Task<ActionResult<ResponseDto>> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointments();
            _responseDto.Data = appointments;
            _responseDto.Message = "Appointments fetched successfully";
            return Ok(_responseDto);
        }

        // Get appointment by id
        [HttpGet("{id})")]
        public async Task<ActionResult<ResponseDto>> GetAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.GetAppointmentById(id);
            _responseDto.Data = appointment;
            _responseDto.Message = "Appointment fetched successfully";
            return Ok(_responseDto);
        }

        // Get appointment by patient id
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<ResponseDto>> GetAppointmentByPatientId(Guid patientId)
        {
            var appointment = await _appointmentService.GetAppointmentByPatientId(patientId);
            if (appointment != null)
            {
                _responseDto.Data = appointment;
                _responseDto.Message = "Appointment fetched successfully";
                return Ok(_responseDto);
            }
            else if (appointment.Count == 0)
            {
                _responseDto.Message = "No appointments found";
                _responseDto.Data = appointment;
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Message = "Error fetching appointments";
                _responseDto.Message = "Error fetching appointments";
                return BadRequest(_responseDto);
            }
        }

        // Delete appointment by id
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto>> DeleteAppointmentById(Guid id)
        {
            var appointment = await _appointmentService.DeleteAppointment(id);
            if (appointment == "Appointment deleted successfully")
            {
                _responseDto.Message = appointment;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Message = appointment;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }
        // update appointment
        [HttpPut]
        public async Task<ActionResult<ResponseDto>> UpdateAppointment(Appointment appointmentDto)
        {
            var appointment = await _appointmentService.UpdateAppointment(appointmentDto);
            if ( appointment == "Appointment deleted successfully")
            {
                _responseDto.Message = appointment;
                _responseDto.IsSuccess = true;
                return Ok(_responseDto);
            }
            else
            {
                _responseDto.Message = appointment;
                _responseDto.IsSuccess = false;
                return BadRequest(_responseDto);
            }
        }
    }
}
