using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AfyaSoftAppointment.Model;

namespace AfyaSoftAppointment.Services.IService
{
    public interface IAppointmentService
    {
        // create appointment
        Task<string> CreateAppointment(Appointment appointment);

        // Get appointment by id
        Task<Appointment> GetAppointmentById(Guid id);

        // Get all appointments
        Task<List<Appointment>> GetAllAppointments();

        // Delete appointment
        Task<string> DeleteAppointment(Guid id);
        
        // Update appointment
        Task<string> UpdateAppointment(Appointment appointment);
        // Get appointment by patient id
        Task<List<Appointment>> GetAppointmentByPatientId(Guid id);

    }
}