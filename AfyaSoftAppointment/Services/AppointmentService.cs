using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAppointment.Data;
using AfyaSoftAppointment.Model;
using AfyaSoftAppointment.Services.IService;
using AutoMapper;

namespace AfyaSoftAppointment.Services
{
    public class AppointmentService : IAppointmentService
    {

        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public AppointmentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<string> CreateAppointment(Appointment appointment)
        {
            try
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return "Appointment created successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> DeleteAppointment(Guid id)
        {
            try
            {
                // get the post with this id
               var appointment = _context.Appointments.FirstOrDefault(x => x.Id == id);
               if ( appointment != null)
               {
                   _context.Appointments.Remove(appointment);
                   _context.SaveChanges();
                   return "Appointment deleted successfully";
               }
               return "Appointment not found";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                var appointments = _context.Appointments.ToList();
                return appointments;
            }
            catch (Exception ex)
            {
                return new List<Appointment>();
            }
        }

        public async Task<Appointment> GetAppointmentById(Guid id)
        {
            try
            {
                var appointment =  _context.Appointments.FirstOrDefault(x => x.Id == id);
                return  appointment;
            }
            catch (Exception ex)
            {
                return new Appointment();
            }
        }

        public async Task<List<Appointment>> GetAppointmentByPatientId(Guid id)
        {
            try
            {
                var appointments = _context.Appointments.Where(x => x.Patient.Id == id).ToList();
                return appointments;
            }
            catch (Exception ex)
            {
                return new List<Appointment>();
            }
        }

        public async Task<string> UpdateAppointment(Appointment appointment)
        {
            try
            {
                var appointmentToUpdate = _context.Appointments.FirstOrDefault(x => x.Id == appointment.Id);
                appointmentToUpdate.Id = appointment.Id;
                appointmentToUpdate.Date = appointment.Date;
                appointmentToUpdate.Doctor = appointment.Doctor;
                appointmentToUpdate.Patient = appointment.Patient;
                appointmentToUpdate.symptoms = appointment.symptoms;
                appointmentToUpdate.Description = appointment.Description;
                _context.SaveChanges();
                return "Appointment updated successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
